using IResult = PokemonReviewApp.Interfaces.IResult;

namespace PokemonReviewApp.Result_Error.Result
{
    public abstract class ResultBase : IResult
    {
        public bool IsSuccess { get; init; }
        public Error? Error{ get; init; }
        protected ResultBase(bool isSuccess, Error? error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }

    //to handle normal success
    public class Result: ResultBase 
    {
        public Success? SuccessResponse { get; init; }
        private Result(bool isSuccess, Success? success = null, Error? error = null  )
            : base(isSuccess,error)
        {
            SuccessResponse = success;
        }
        public static Result Success(Success success) => new(true, success);
        public static Result Faliure(Error error) => new(false, error: error);
    }

    //to handle genric success 
    public class Result<T> : ResultBase
    {
        public Success<T>? SuccessResponse { get; init; }
        private Result(bool isSuccess, Success<T>? success = null, Error? error = null)
            : base(isSuccess, error)
        {
            SuccessResponse = success;
        }
        public static Result<T> Success(Success<T> success) => new(true, success);
        public static Result<T> Faliure(Error error) => new(false, error: error);

    }
}
