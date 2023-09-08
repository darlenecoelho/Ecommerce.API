using AutoMapper;
using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, ReadCategoryDTO>()
        .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories))
        .ReverseMap();
        CreateMap<CreateCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();
        CreateMap<Subcategory, ReadSubcategoryDTO>()
            .ReverseMap();
        CreateMap<CreateSubcategoryDTO, Subcategory>()
            .ReverseMap();
    }
}
