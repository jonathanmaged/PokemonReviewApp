﻿using System.Diagnostics.Metrics;
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
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICountryService countryService;
        private readonly IMapper mapper;

        public OwnerService(IUnitOfWork unitOfWork, ICountryService countryService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.countryService = countryService;
            this.mapper = mapper;
        }

        public async Task<ICollection<OwnerDto>> GetOwnersAsync()
        {
            var owners = await unitOfWork.OwnerRepository.GetAll();
            var ownersDto = mapper.Map<ICollection<OwnerDto>>(owners);
            return ownersDto;
        }

        public async Task<OwnerDto?> GetOwnerByIdAsync(int id)
        {
            var owner = await unitOfWork.OwnerRepository.GetById(id);
           
            var ownerDto = mapper.Map<OwnerDto>(owner);
            return ownerDto;
        }

        public async Task<ICollection<OwnerDto>> GetOwnersOfAPokemonAsync(int pokeId)
        {
            var owners = await unitOfWork.OwnerRepository.GetOwnersOfAPokemonAsync(pokeId);
            var ownersDto = mapper.Map<ICollection<OwnerDto>>(owners);
            return ownersDto;

        }

        public async Task<ICollection<PokemonDto>> GetPokemonsByOwnerAsync(int ownerId)
        {
            var pokemons = await unitOfWork.OwnerRepository.GetPokemonsByOwnerAsync(ownerId);
            var pokemonsDto = mapper.Map<ICollection<PokemonDto>>(pokemons);
            return pokemonsDto;
        }

        public async Task<OneOf<Owner, ConflictError<Owner>, DatabaseError,NotFoundError>> CreateOwnerAsync(CreateOwnerDto createOwnerDto, string countryName)
        {
            var conflictCheck = await CheckConflictAsync(createOwnerDto.LastName);
            if (conflictCheck is not null){return conflictCheck;}

            var owner = mapper.Map<Owner>(createOwnerDto);

            var DatabaseError = await HandleCountryAssignmentAsync(countryName,owner);
            if (DatabaseError is not null) { return DatabaseError; }

            var NotFoundError = await HandlePokemonOwnerAssignment(createOwnerDto.PokemonsId, owner);
            if (NotFoundError is not null) { return NotFoundError; }

            unitOfWork.OwnerRepository.Add(owner);

            var saved = await unitOfWork.Save();
            if (saved == 0) return new DatabaseError("Something Went wrong when saving in the database");

            return owner;

        }
        private async Task<ConflictError<Owner>?> CheckConflictAsync(string lastName) 
        {
            var owner = await unitOfWork.OwnerRepository.GetOwnerByNameAsync(lastName);

            if (owner != null)
            {
                return new ConflictError<Owner>("Owner Already Exists", owner);
            }
            return null;
        }
        private async Task<DatabaseError?> HandleCountryAssignmentAsync(string countryName,Owner owner) 
        {
            var countryDto = new CreateCountryDto { Name = countryName };
            var result = await countryService.CreateCountryAsync(countryDto);

            if (result.IsT2)
            {
                return new DatabaseError("Something Went wrong when saving in the database");
            }
            if (result.IsT0)
            {
                owner.Country = result.AsT0;
            }
            else if (result.IsT1)
            {
                owner.Country = result.AsT1.Entity;

            }
            return null;
        }
        private async Task<NotFoundError?> HandlePokemonOwnerAssignment(IEnumerable<int> pokemonsId, Owner owner)
        {
            var exists = await unitOfWork.PokemonRepository.CheckListOfPokemonIdAsync(pokemonsId);
            if (!exists) { return new NotFoundError("Not all Pokemonid Exists"); }
            owner.OwnerPokemons = new List<PokemonOwner>();
            foreach (int id in pokemonsId) {
  
                owner.OwnerPokemons.Add(new PokemonOwner { OwnerId = owner.Id, PokemonId = id}); 
            }
            return null;
         }

        public async Task<OneOf<Owner, NotFoundError, DatabaseError>> UpdateOwnerAsync(OwnerDto ownerDto)
        {
            var country = await unitOfWork.CountryRepository.GetById(ownerDto.CountryId.Value);
            if (country is null)
                return new NotFoundError("country id provided doesnt exist in the database");

            var owner = await unitOfWork.OwnerRepository.GetById(ownerDto.Id.Value);
            if (owner is null)
                return new NotFoundError("owner id provided doesnt exist in the database");

            mapper.Map(ownerDto, owner);

            unitOfWork.OwnerRepository.Update(owner);
            var saved = await unitOfWork.Save();
            if (saved == 0)
                return new DatabaseError("Couldnt save in the database");
            return owner;
        }
        public async Task<OneOf<Owner, NotFoundError, DatabaseError>> DeleteOwnerAsync(int id)
        {
            var owner = await unitOfWork.OwnerRepository.GetById(id);
            if (owner is null) return new NotFoundError("owner with that id doesnt exist");

            unitOfWork.OwnerRepository.Delete(owner);
            var saved = await unitOfWork.Save();
            if (saved == 0) return new DatabaseError("Error Happen When Deleting Entity From The Database");
            return owner;
        }
    }
}
