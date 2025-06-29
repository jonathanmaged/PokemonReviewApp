using PokemonReviewApp.Dto.UserDto;

namespace PokemonReviewApp.Result_Error
{
    public class AuthResponse
    {
        // error response

        public static readonly Error NotAuthorized = new(401, "user is not authorized to access");
        public static Error EmailAlreadyExist(string email) => new(409, errorList: [$"{email} is already Exists"]);
        public static Error RegisterUserError(IEnumerable<string> errors) => new(400, errorList: errors);

        // success response

        public static readonly Success RegisterSuccessfully = new(201, "user registered succefully");
        public static Success<TokenPairDto> LoginSuccessfully(TokenPairDto dto) => new(201, dto);
        public static Success<TokenPairDto> RefreshTokenValidated(TokenPairDto dto) => new(201, dto);

        public static readonly Success LogoutSuccessfully = new(200, "user logout succefully");
        public static readonly Success AlreadyLoggedOut = new(200, "user already Logged Out");


    }
}
