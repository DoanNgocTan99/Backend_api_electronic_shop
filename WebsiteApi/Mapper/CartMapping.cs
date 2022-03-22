using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Mapper
{
    public class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<CartDto, Cart>();
            CreateMap<Cart, CartDto>();
        }
    }
}
