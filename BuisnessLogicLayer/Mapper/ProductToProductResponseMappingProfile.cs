using AutoMapper;
using Ecommerce.BuisnessLogicLayer.DTO;
using Ecommerce.DataAccessLayer.Entities;

namespace Ecommerce.BuisnessLogicLayer.Mapper;

public class ProductToProductResponseMappingProfile:Profile
{
    public ProductToProductResponseMappingProfile() 
    {
        CreateMap<Product, ProductResponse>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));
        CreateMap<Product, ProductResponse>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        CreateMap<Product, ProductResponse>().ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        CreateMap<Product, ProductResponse>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));   
        CreateMap<Product, ProductResponse>().ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
