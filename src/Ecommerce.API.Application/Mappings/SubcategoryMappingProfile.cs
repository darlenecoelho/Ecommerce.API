using AutoMapper;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;
public class SubcategoryMappingProfile : Profile
{
    public SubcategoryMappingProfile()
    {
        CreateMap<Subcategory, ReadSubcategoryDTO>()
          .ReverseMap();
        CreateMap<CreateSubcategoryDTO, Subcategory>()
            .ReverseMap();
        CreateMap<UpdateSubcategoryDTO, Subcategory>()
            .ReverseMap();
    }
}
