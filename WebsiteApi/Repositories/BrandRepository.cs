using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
namespace WebsiteApi.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApiContext _context;
        public BrandRepository(ApiContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var brand = this.GetById(id);
            if (brand == null)
                throw new IsNotExist("There is no Brand with Id is " + id);
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return "Delete successfully";
        }

        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public Brand GetById(int id)
        {
            return _context.Brands.Where(x => x.Id == id).FirstOrDefault();
        }

        public Brand Update(int id, Brand brand)
        {
            var bra = this.GetById(id);
            bra.Name = brand.Name;
            bra.CreatedBy = brand.CreatedBy;
            bra.ModifiedBy = brand.ModifiedBy;
            bra.CreatedDate = brand.CreatedDate;
            bra.ModifiedDate= DateTime.Now;
            bra.Del = brand.Del;
            _context.SaveChanges();
            return bra;
        }
        public bool CheckName(string name)
        {
            return _context.Brands.Where(x => x.Name.ToUpper().Equals(name.ToUpper())).FirstOrDefault() == null ? true : false;
        }

        public Brand Create(Brand brand)
        {
            _context.Brands.Add(brand);
            brand.CreatedDate = DateTime.Now;
            _context.SaveChanges();
            return brand;
        }
    }
}
