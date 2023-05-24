using AutoMapper;
using Ecommerce.API.Application.DTOs;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.MappingProfiles;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ReadProductDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory));

        CreateMap<CreateProductDTO, Product>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.SubcategoryId));
    }
}
