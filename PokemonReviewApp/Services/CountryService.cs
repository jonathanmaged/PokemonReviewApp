using AutoMapper;
using OneOf;
using PokemonReviewApp.Dto.CreateDto;
using PokemonReviewApp.Dto.GetDto;
using PokemonReviewApp.Errors;
using PokemonReviewApp.Interfaces.Repository;
using PokemonReviewApp.Interfaces.Services;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Services
{
    public class CountryService:ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<ICollection<CountryDto>> GetCountriesAsync()
        {
            var countries = await unitOfWork.CountryRepository.GetAll();
            var countriesDto = mapper.Map<List<CountryDto>>(countries);
            return countriesDto;
        }

        public async Task<CountryDto?> GetCountryByIdAsync(int id)
        {
            var country = await unitOfWork.CountryRepository.GetById(id);
            var countryDto = mapper.Map<CountryDto>(country);
            return countryDto;
        }

        public async Task<CountryDto?> GetCountryOfAnOwnerAsync(int ownerId)
        {
            var country = await unitOfWork.CountryRepository.GetCountryByOwnerAsync(ownerId);
            var countryDto = mapper.Map<CountryDto>(country);
            return countryDto;
        }
        public async Task<OneOf<Country, ConflictError<Country>, DatabaseError>> CreateCountryAsync(CreateCountryDto countryDto)
        {
            var country = await unitOfWork.CountryRepository.GetCountryByNameAsync(countryDto.Name);

            if (country != null)
            {
                return new ConflictError<Country>("Country Already Exists",country);
            }
            country = mapper.Map<Country>(countryDto);

            unitOfWork.CountryRepository.Add(country);

            var saved = await unitOfWork.Save();                  
            if (saved == 0) return new DatabaseError("Something Went wrong when saving in the database");

            return country;
        }

        public async Task<OneOf<Country, NotFoundError, DatabaseError>> UpdateCountryAsync(CountryDto countryDto)
        {
    
            var country = await unitOfWork.CountryRepository.GetById(countryDto.Id.Value);
            if (country is null)
                return new NotFoundError("country id provided doesnt exist in the database");

            mapper.Map(countryDto, country);

            unitOfWork.CountryRepository.Update(country);
            var saved = await unitOfWork.Save();
            if (saved == 0)
                return new DatabaseError("Couldnt save in the database");
            return country;
        }
        public async Task<OneOf<Country, NotFoundError, DatabaseError>> DeleteCountryAsync(int id)
        {
            var country = await unitOfWork.CountryRepository.GetById(id);
            if (country is null) return new NotFoundError("country with that id doesnt exist");

            unitOfWork.CountryRepository.Delete(country);
            var saved = await unitOfWork.Save();
            if (saved == 0) return new DatabaseError("Error Happen When Deleting Entity From The Database");
            return country;
        }
    }
}
    

