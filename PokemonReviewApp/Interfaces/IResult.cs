
using PokemonReviewApp.Result_Error;

namespace PokemonReviewApp.Interfaces
{
    public interface IResult
    {
        public bool IsSuccess { get; init; }
        public Error? Error { get; init; }

    }
}
