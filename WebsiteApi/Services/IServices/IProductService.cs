using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
        ProductDto Create(ProductDto category);
        ProductDto Update(int id, ProductDto category);
        string Delete(int id);
        IEnumerable<ProductDto> GetAllByCategory(string categoryName);
        IEnumerable<ProductDto> GetRandom();
        IEnumerable<ProductDto> GetListProductByCategory(ProductListByCategory value);


    }
}
