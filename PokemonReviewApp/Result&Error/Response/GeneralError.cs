namespace PokemonReviewApp.Result_Error
{
    public class GeneralError
    {
        public static readonly Error ConflictError = new(409, "user already exists");
        public static readonly Error BadRequestError = new(400);
    }
}
