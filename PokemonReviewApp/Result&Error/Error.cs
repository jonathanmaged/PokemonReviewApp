namespace PokemonReviewApp.Result_Error
{
    public class Error (int statusCode, string message = "", IEnumerable<string>? errorList = null) 
        : Base(statusCode,message)
    {
        public IEnumerable<string> ErrorList { get; } = errorList ?? Enumerable.Empty<string>();


    } 
}
