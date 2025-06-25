using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;
using PokemonReviewApp.Result_Error;
using PokemonReviewApp.Result_Error.Result;

namespace PokemonReviewApp.Services
{
    public class AuthService(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMapper mapper) : IAuthService
    {

        public async Task<ServiceResponse> RegisterAsync(RegisterDto registerDto)
        {
            var user = await userManager.FindByEmailAsync(registerDto.Email);
            if(user is not null) return new ServiceResponse(409, "User Already Exists");

            user = mapper.Map<ApplicationUser>(registerDto);
            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) {
                var errors = result.Errors.Select(e => e.Description);
                return new ServiceResponse(statusCode: 400, entity: errors); 
            }
            return new ServiceResponse(201,"User created successfully",user);

        }

        public async Task<Result<TokenPairDto>> LoginAsync(LoginDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) return Result<TokenPairDto>.Faliure(AuthError.NotAuthorized);

            var authenticated= await userManager.CheckPasswordAsync(user,request.Password);
            if(!authenticated) return Result<TokenPairDto>.Faliure(AuthError.NotAuthorized);

            string token = await GenerateTokenAsync(user);
            string refreshToken = await CreateAndSaveRefreshToken(user);

            return Result<TokenPairDto>.Success(new(token,refreshToken));
        }

        public async Task<Result<TokenPairDto>> ValidateRefreshToken(RefreshTokenDto request)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken);
            if(refreshToken is null || !refreshToken.IsActive) return Result<TokenPairDto>.Faliure(AuthError.NotAuthorized);

            refreshToken.IsUsed = true;
            string newToken = await GenerateTokenAsync(refreshToken.User);
            string newRefreshToken = await CreateAndSaveRefreshToken(refreshToken.User);

            return Result<TokenPairDto>.Success(new(newToken, newRefreshToken));
            
        }
        public async Task<ServiceResponse> LogoutAsync(string publicId)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetByPublicIdAsync(publicId);
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
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.PublicId.ToString())
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
