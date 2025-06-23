namespace PokemonReviewApp.Interfaces.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICountryRepository CountryRepository { get; }
        IOwnerRepository OwnerRepository { get; }
        IPokemonRepository PokemonRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task<int> Save();
    }
}
