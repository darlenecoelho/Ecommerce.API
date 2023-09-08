using AutoMapper;
using Ecommerce.API.Application.DTOs.Product;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ReadProductDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory))
            .ReverseMap();

        CreateMap<CreateProductDTO, Product>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.SubcategoryId));

        CreateMap<UpdateProductDTO, Product>().ReverseMap();
    }
}
