using AutoMapper;
using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Domain.Entities;

namespace Ecommerce.API.Application.Mappings;
public class SubcategoryMappingProfile : Profile
{
    public SubcategoryMappingProfile()
    {
        CreateMap<Subcategory, ReadSubcategoryDTO>();
        CreateMap<CreateSubcategoryDTO, Subcategory>();
        CreateMap<UpdateSubcategoryDTO, Subcategory>();
        CreateMap<CreateSubcategoryCommand, Subcategory>();
    }
}
