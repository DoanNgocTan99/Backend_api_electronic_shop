using System.Collections.Generic;
using System.Linq;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApiContext _context;
        public RoleRepository(ApiContext context)
        {
            _context = context;
        }
        public ICollection<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
    }
}
