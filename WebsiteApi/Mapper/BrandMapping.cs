using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Mapper
{
    public class BrandMapping: Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
        }
    }
}
