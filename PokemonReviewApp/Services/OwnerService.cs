using System.Diagnostics.Metrics;
using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICountryService countryService;
        private readonly IMapper _mapper;

        public OwnerService(IUnitOfWork unitOfWork, ICountryService countryService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.countryService = countryService;
            _mapper = mapper;
        }

        public async Task<ICollection<OwnerDto>> GetOwnersAsync()
        {
            var owners = await unitOfWork.OwnerRepository.GetAll();
            var ownersDto = _mapper.Map<ICollection<OwnerDto>>(owners);
            return ownersDto;
        }

        public async Task<OwnerDto?> GetOwnerByIdAsync(int id)
        {
            var owner = await unitOfWork.OwnerRepository.GetById(id);
            var ownerDto = _mapper.Map<OwnerDto>(owner);
            return ownerDto;
        }

        public async Task<ICollection<OwnerDto>> GetOwnersOfAPokemonAsync(int pokeId)
        {
            var owners = await unitOfWork.OwnerRepository.GetOwnersOfAPokemonAsync(pokeId);
            var ownersDto = _mapper.Map<ICollection<OwnerDto>>(owners);
            return ownersDto;

        }

        public async Task<ICollection<PokemonDto>> GetPokemonsByOwnerAsync(int ownerId)
        {
            var pokemons = await unitOfWork.OwnerRepository.GetPokemonsByOwnerAsync(ownerId);
            var pokemonsDto = _mapper.Map<ICollection<PokemonDto>>(pokemons);
            return pokemonsDto;
        }

        public async Task<OneOf<Owner, ConflictError, DatabaseError>> CreateOwnerAsync(OwnerDto ownerDto, string countryName)
        {
            var owner = await unitOfWork.OwnerRepository.GetOwnerByNameAsync(ownerDto.LastName);

            if (owner != null)
            {
                return new ConflictError("Owner Already Exists");
            }

            owner = _mapper.Map<Owner>(ownerDto);

            var countryDto = new CountryDto { Name = countryName };
            var result = await countryService.CreateCountryAsync(countryDto);

            if (result.IsT2)
            { 
                    return new DatabaseError("Something Went wrong when saving in the database");
            }
            if (result.IsT0)
            {
                owner.Country = result.AsT0;
            }else if (result.IsT1) 
            {
                owner.Country = await unitOfWork.CountryRepository.GetCountryByNameAsync(countryName); 

            }
            unitOfWork.OwnerRepository.Add(owner);

            var saved = await unitOfWork.Save();
            if (saved == 0)
                return new DatabaseError("Something Went wrong when saving in the database");
            return owner;

        }
    }
}
