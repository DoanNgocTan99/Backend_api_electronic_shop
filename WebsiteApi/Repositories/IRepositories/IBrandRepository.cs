using System.Collections.Generic;
using WebsiteApi.Model.Entity;
namespace WebsiteApi.Repositories.IRepositories
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAll();
        Brand GetById(int id);
        string Delete(int id);
        Brand Update(int id, Brand brand);

        Brand Create (Brand brand);
        bool CheckName(string name);
    }
}
