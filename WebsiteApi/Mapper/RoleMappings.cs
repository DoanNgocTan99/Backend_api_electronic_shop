

using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Mapper
{
    public class RoleMappings : Profile
    {
        public RoleMappings()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
