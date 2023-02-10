using AutoMapper;
using MSA.Services.ShoppingCartAPI.Models;
using MSA.Services.ShoppingCartAPI.Models.Dto;

namespace MSA.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
            });
            return mapperConfig;
        }
    }
}
