using System.Collections.Generic;
using WebsiteApi.Model.Entity;
namespace WebsiteApi.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Create(Category category);
        Category Update(int id, Category category);
        string Delete(int id);
    }
}
