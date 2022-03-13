using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services.IServices
{
    public interface IBrandService
    {
        IEnumerable<BrandDto> GetAll();
        BrandDto GetById(int id);
        string Delete(int id);
        BrandDto Update(int id, BrandDto brand);
        BrandDto Create(BrandDto brand);
    }
}
