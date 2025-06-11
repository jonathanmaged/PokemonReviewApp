using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces.Services
{
    public interface IOwnerService
    {
        Task<OneOf<Owner, ConflictError, DatabaseError>> CreateOwnerAsync(OwnerDto ownerDto, string countryName);
    }
}
