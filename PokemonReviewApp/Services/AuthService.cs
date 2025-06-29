using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Dto.AuthDto;
using PokemonReviewApp.Dto.UserDto;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Result_Error;
using PokemonReviewApp.Result_Error.Result;
using IResult = PokemonReviewApp.Interfaces.IResult;


namespace PokemonReviewApp.Services
{
    public class AuthService(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        TokensService tokensSerivce,
        IMapper mapper) : IAuthService
    {

        public async Task<Result> RegisterAsync(RegisterDto registerDto)
        {
            var user = await userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null) return Result.Faliure(AuthResponse.EmailAlreadyExist(registerDto.Email));


            user = mapper.Map<ApplicationUser>(registerDto);
            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) {

                var errors = result.Errors;

                var dublicateUserError = errors
                    .Where(e => e.Code == "DuplicateUserName")
                    .Select(e => e.Description);

                if (dublicateUserError.Any()) return Result.Faliure(new(409, errorList: dublicateUserError));

                var descriptions = errors.Select(e => e.Description);
                return Result.Faliure(AuthResponse.RegisterUserError(descriptions)); 
            }
            return Result.Success(AuthResponse.RegisterSuccessfully);

        }

        public async Task<IResult> LoginAsync(LoginDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) return Result.Faliure(AuthResponse.NotAuthorized);

            var authenticated= await userManager.CheckPasswordAsync(user,request.Password);
            if(!authenticated) return Result.Faliure(AuthResponse.NotAuthorized);

            string token = await tokensSerivce.GenerateTokenAsync(user);
            string refreshToken =  tokensSerivce.CreateAndAddRefreshToken(user);

            await unitOfWork.Save();

            return Result<TokenPairDto>.Success(AuthResponse.LoginSuccessfully(new(token,refreshToken)));
        }

        public async Task<IResult> ValidateRefreshToken(RefreshTokenDto request)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken);
            if(refreshToken is null || !refreshToken.IsActive) return Result.Faliure(AuthResponse.NotAuthorized);

            refreshToken.IsUsed = true;

            string newToken = await tokensSerivce.GenerateTokenAsync(refreshToken.User);
            string newRefreshToken = tokensSerivce.CreateAndAddRefreshToken(refreshToken.User);

            await unitOfWork.Save();

            return Result<TokenPairDto>.Success(AuthResponse.RefreshTokenValidated(new(newToken,newRefreshToken)));

        }
        public async Task<IResult> LogoutAsync(string publicId)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetByPublicIdAsync(publicId);
            if (refreshToken is null || !refreshToken.IsActive) return Result.Success(AuthResponse.AlreadyLoggedOut);

            refreshToken.IsRevoked = true;
            await unitOfWork.Save();
            return Result.Success(AuthResponse.LogoutSuccessfully);
        }

    }
}
