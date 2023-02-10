using AutoMapper;
using MSA.Services.CouponAPI.Models;
using MSA.Services.CouponAPI.Models.Dto;

namespace MSA.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });
            return mapperConfig;
        }
    }
}
