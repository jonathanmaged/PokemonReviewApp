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
    public class AuthService(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMapper mapper) : IAuthService
    {

        public async Task<ServiceResponse> RegisterAsync(UserDto userDto)
        {
            var user = await userManager.FindByEmailAsync(userDto.Email);
            if(user is not null) return new ServiceResponse(409, "User Already Exists");

            user = mapper.Map<ApplicationUser>(userDto);
            var result = await userManager.CreateAsync(user,userDto.Password);
            if (!result.Succeeded)
                return new ServiceResponse(500, "enter valid Data");
            return new ServiceResponse(201,"User created successfully",user);

        }

        public async Task<ServiceResponse> LoginAsync(UserDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) return new ServiceResponse(401,"unauthorized access");

            var authenticated= await userManager.CheckPasswordAsync(user,request.Password);
            if(!authenticated) return new ServiceResponse(401, "unauthorized access");

            string token = await GenerateTokenAsync(user);
            string refreshToken = await CreateAndSaveRefreshToken(user);

            return new ServiceResponse(statusCode:200,entity:new TokenPairDto{Token=token,RefreshToken= refreshToken });
        }

        public async Task<ServiceResponse> ValidateRefreshToken(RefreshTokenDto request)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken);
            if(refreshToken is null || !refreshToken.IsActive) return new ServiceResponse(401, "NotAuthorized");

            refreshToken.IsUsed = true;
            string newToken = await GenerateTokenAsync(refreshToken.User);
            string newRefreshToken = await CreateAndSaveRefreshToken(refreshToken.User);

            return new ServiceResponse(statusCode: 200, entity: new TokenPairDto { Token = newToken, RefreshToken = newRefreshToken });
            
        }
        public async Task<ServiceResponse> LogoutAsync(RefreshTokenDto request)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken);
            if (refreshToken is null || !refreshToken.IsActive) return new ServiceResponse(401, "NotAuthorized");

            refreshToken.IsRevoked = true;
            return new ServiceResponse(statusCode: 200,"Logout Sucssefully");
        }
         
        private async Task<string> CreateAndSaveRefreshToken(ApplicationUser user) 
        { 
            var refreshToken = GenerateRefreshToken();
            var refreshTokenObject = new RefreshToken
            {
                Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpireDays")),
                User = user
            };

            unitOfWork.RefreshTokenRepository.Add(refreshTokenObject);
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

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName!),
                };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:TokenExpireMinutes")),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
