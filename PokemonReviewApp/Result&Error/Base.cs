namespace PokemonReviewApp.Result_Error
{
    public abstract class Base
    {
        protected Base(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public int StatusCode { get; }
        public string Message { get; }
    }
}
