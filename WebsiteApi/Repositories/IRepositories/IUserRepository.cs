using System.Collections.Generic;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        string Delete(int id);
        bool check(string Email = "", string UserName = "", string Phone = "");
        User Update(int id, User user);
        string ChangePassword(int id, string password);
    }
}
