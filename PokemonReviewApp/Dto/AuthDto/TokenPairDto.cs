namespace PokemonReviewApp.Dto.UserDto
{
    public class TokenPairDto
    {
        public TokenPairDto(string token, string refreshToken )
        {
            Token = token;
            RefreshToken = refreshToken;
        }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
