using System.Collections.Generic;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product category);
        Product Update(int id, Product category);
        string Delete(int id);
    }
}
