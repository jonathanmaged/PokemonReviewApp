using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class AuthService(IUnitOfWork unitOfWork,IConfiguration configuration ) : IAuthService
    {
        public async Task<ServiceResponse> RegisterAsync(UserDto request)
        {
            var exits = await unitOfWork.UserRepository.IsUserExistAsync(request.UserName);
            if(exits) return new ServiceResponse(409, "User Already Exists");

            var user = new User();
            var PasswordHash = new PasswordHasher<User>().HashPassword(user,request.Password);
            user.UserName = request.UserName;
            user.PasswordHash = PasswordHash;

            unitOfWork.UserRepository.Add(user);
            await unitOfWork.Save();
            return new ServiceResponse(201,"User created successfully",user);

        }
        public async Task<ServiceResponse> LoginAsync(UserDto request)
        {
            var user = await unitOfWork.UserRepository.GetUserByUserName(request.UserName);
            if (user == null) return new ServiceResponse(401,"unauthorized access");

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if(result == PasswordVerificationResult.Failed) return new ServiceResponse(401, "unauthorized access");

            string token = GenerateToken(user);
            string refreshToken = await CreateAndSaveRefreshToken(user);

            return new ServiceResponse(statusCode:200,entity:new TokenPairDto{Token=token,RefreshToken= refreshToken });
        }
        public async Task<ServiceResponse> ValidateRefreshToken(RefreshTokenRequestDto request) 
        {
            var user = await unitOfWork.UserRepository.GetUserByUserNameAsNoTracking(request.UserName);
            if (user == null || user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryDate < DateTime.UtcNow) 
            {
                return new ServiceResponse(401,"NotAuthorized");
            }
            string newToken =  GenerateToken(user);
            return new ServiceResponse(200,newToken);
        }
        private async Task<string> CreateAndSaveRefreshToken(User user) 
        { 
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
            await unitOfWork.Save();
            return refreshToken;
            
        }
        private string GenerateRefreshToken() 
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,user.Role)
                };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
