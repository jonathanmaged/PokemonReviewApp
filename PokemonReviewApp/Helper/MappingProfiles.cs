using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Owner, SimpleOwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Pokemon, PokemonDto>()
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src =>src.Category.Name))
                .ForMember(dest => dest.Owners,opt => opt.MapFrom(src => src.PokemonOwners.Select(po => po.Owner)) )
                .ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
                
        }
    }
}
