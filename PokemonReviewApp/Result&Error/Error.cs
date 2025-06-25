namespace PokemonReviewApp.Result_Error
{
    public sealed record NewError(int StatusCode, string Message)
    {
        public static readonly NewError None = new(200, string.Empty); 
    } 
}
