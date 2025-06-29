namespace PokemonReviewApp.Result_Error
{
    public class Success(int statusCode, string message = "")
    : Base(statusCode, message)
    {
    }
    public class Success<T> (int statusCode, T entity, string message = "") 
        :Base(statusCode,message)
    {
        public T Entity { get; set; } = entity;
    }
}
