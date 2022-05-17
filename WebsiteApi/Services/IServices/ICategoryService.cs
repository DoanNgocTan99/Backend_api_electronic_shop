using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        CategoryDto Create(CategoryDto category);
        CategoryDto Update(int id, CategoryDto category);
        string Delete(int id);
        string UploadImage(string path);
    }
}
