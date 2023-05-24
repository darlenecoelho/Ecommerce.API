using AutoMapper;
using Ecommerce.API.Application.DTOs;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, ReadCategoryDTO>()
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories));

        CreateMap<CreateCategoryDTO, Category>();

        CreateMap<Subcategory, ReadSubcategoryDTO>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<CreateSubcategoryDTO, Subcategory>();
    }
}
