using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;
public class SubcategoryMappingProfile : Profile
{
    public SubcategoryMappingProfile()
    {
        CreateMap<Subcategory, ReadSubcategoryDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<CreateSubcategoryDTO, Subcategory>();
        CreateMap<UpdateSubcategoryDTO, Subcategory>();
    }
}
