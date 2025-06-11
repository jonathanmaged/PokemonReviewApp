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
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryService countryService;
        private readonly ICountryRepository countryRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository,ICountryService countryService,ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            this.countryService = countryService;
            this.countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<Owner, ConflictError, DatabaseError>> CreateOwnerAsync(OwnerDto ownerDto, string countryName)
        {
            var owner = await _ownerRepository.GetOwnerByNameAsync(ownerDto.LastName);

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
                owner.Country = await countryRepository.GetCountryByNameAsync(countryName); 

            }

            var saved = await _ownerRepository.CreateOwnerAsync(owner);
            if (!saved)
                return new DatabaseError("Something Went wrong when saving in the database");
            return owner;

        }

    }
}
