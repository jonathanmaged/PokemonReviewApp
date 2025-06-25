namespace PokemonReviewApp.Result_Error
{
    public class AuthError
    {
        public static readonly NewError NotAuthorized = new(401,"user is not authorized to access");
    }
}
