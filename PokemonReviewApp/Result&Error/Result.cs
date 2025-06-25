namespace PokemonReviewApp.Result_Error.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsFaliure => !IsSuccess;
        public T? Entity { get; set; }
        public NewError Error { get; set; }

        private Result(bool isSuccess,NewError error,T? entity = default)
        {
            IsSuccess = isSuccess;
            Error = error;
            Entity = entity;
        }
        public static Result<T> Success(T entity) => new Result<T>(true,NewError.None,entity);
        public static Result<T> Faliure(NewError error) => new Result<T>(false,error);

    }
}
