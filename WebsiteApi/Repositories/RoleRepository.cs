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
        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role Update(int id, Role role)
        {
            var _role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
            _role.Name = role.Name;
            _role.Description = role.Description;
            _role.ModifiedDate = System.DateTime.Now;
            _context.SaveChanges();
            return _role;
        }
    }
}
