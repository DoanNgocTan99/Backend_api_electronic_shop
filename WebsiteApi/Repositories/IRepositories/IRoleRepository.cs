using System.Collections.Generic;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role Update(int id, Role role);
    }
}
