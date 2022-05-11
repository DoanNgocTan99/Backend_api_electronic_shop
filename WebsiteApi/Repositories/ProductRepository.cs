using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
namespace WebsiteApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiContext _context;
        public ProductRepository(ApiContext _context)
        {
            this._context = _context;
        }
        public Product Create(Product product)
        {
            if (_context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault() != null)
            {
                throw new IsExist(product.Name + " already exists in the database");
            }
            product.CreatedDate = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public string Delete(int id)
        {
            //Ver1:
            var product = this.GetById(id);
            if (product == null)
                throw new IsNotExist("There is no Product with Id is " + id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return "Delete successfully";
            //Ver2:
            //var Product = this.GetById(id);
            //if (Product == null)
            //    throw new IsNotExist("There is no Product with Id is " + id);
            //Product.Del = true;
            //_context.SaveChanges();
            //return "Delete successfully";
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.OrderByDescending(x => x.Id).ToList();
        }

        public string GetBrand(long id)
        {
            try
            {
                var temp = _context.Brands.Where(p => p.Id == id).FirstOrDefault();
                if (temp != null)
                {
                    return temp.Name;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public Product GetById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
                throw new IsNotExist("There is no Brand with Id is " + id);
            return product;
        }

        public string GetCategory(long id)
        {
            try
            {
                var temp = _context.Categories.Where(p => p.Id == id).FirstOrDefault();
                if (temp != null)
                {
                    return temp.Name;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetPath(long id)
        {
            try
            {
                var temp = _context.ProductImages.Where(p => p.ProductId == id).FirstOrDefault();
                if (temp != null)
                {
                    return temp.Path;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public Product Update(int id, Product product)
        {
            var _product = this.GetById(id);
            if (!string.Equals(_product.Name, product.Name))
            {
                if (_context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault() != null)
                {
                    throw new IsExist("\" " + product.Name + " \" already exists in the database");
                }
            }
            _product.ModifiedDate = DateTime.Now;

            _product.Name = product.Name;
            _product.Description = product.Description;
            _product.Material = product.Material;
            _product.Origin = product.Origin;
            _product.Product_Price = product.Product_Price;
            _product.Del_Price = product.Del_Price;
            _product.WarrantyDate = product.WarrantyDate;
            _product.Stock = product.Stock;
            _product.Discount = product.Discount;
            _product.Views = product.Views;
            _product.Rate = product.Rate;
            _product.IsActive = product.IsActive;

            _product.BrandId = product.BrandId;
            _product.CategoryId = product.CategoryId;

            _context.SaveChanges();
            return _product;
        }
    }
}
