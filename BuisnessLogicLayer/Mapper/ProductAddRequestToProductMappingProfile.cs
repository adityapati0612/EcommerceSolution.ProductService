using AutoMapper;
using Ecommerce.DataAccessLayer.Entities;
using Ecommerce.BuisnessLogicLayer.DTO;
using Ecommerce.BuisnessLogicLayer.Validators;

namespace Ecommerce.BuisnessLogicLayer.Mapper;

public class ProductAddRequestToProductMappingProfile: Profile
{
    public ProductAddRequestToProductMappingProfile() 
    {
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.ProductName,opt=>opt.MapFrom(src => src.ProductName));
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.ProductId, opt => opt.Ignore());
        CreateMap<ProductAddRequest, Product>().ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
