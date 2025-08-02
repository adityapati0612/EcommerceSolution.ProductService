using AutoMapper;
using Ecommerce.BuisnessLogicLayer.DTO;
using Ecommerce.DataAccessLayer.Entities;

namespace Ecommerce.BuisnessLogicLayer.Mapper;

public class ProductUpdateRequestToProductMappingProfile: Profile
{
    public ProductUpdateRequestToProductMappingProfile()
    {

        CreateMap<ProductUpdateRequest, Product>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));
        CreateMap<ProductUpdateRequest, Product>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        CreateMap<ProductUpdateRequest, Product>().ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        CreateMap<ProductUpdateRequest, Product>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID));
        CreateMap<ProductUpdateRequest, Product>().ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
