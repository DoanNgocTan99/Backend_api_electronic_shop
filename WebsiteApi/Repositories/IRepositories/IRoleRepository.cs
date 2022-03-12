using System.Collections.Generic;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        ICollection<Role> GetAll();

    }
}
