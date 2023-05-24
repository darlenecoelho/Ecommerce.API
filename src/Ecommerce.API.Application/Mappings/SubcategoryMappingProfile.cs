using AutoMapper;
using Ecommerce.API.Application.DTOs;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.MappingProfiles;
public class SubcategoryMappingProfile : Profile
{
    public SubcategoryMappingProfile()
    {
        CreateMap<Subcategory, ReadSubcategoryDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<CreateSubcategoryDTO, Subcategory>();
    }
}
