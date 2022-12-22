using AutoMapper;
using MCA.Services.ProductAPI.Models;
using MCA.Services.ProductAPI.Models.Dto;

namespace MCA.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mapperConfig;
        }
    }
}
