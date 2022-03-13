using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAll();
        RoleDto Update(int id, RoleDto role);
    }
}
