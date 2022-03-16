using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Mapper
{
    public class UserMapping: Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
