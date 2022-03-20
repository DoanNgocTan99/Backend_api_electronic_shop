using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiContext _context;
        public CategoryRepository(ApiContext _context)
        {
            this._context = _context;
        }
        public Category Create(Category category)
        {
            if (_context.Categories.Where(x => x.Name.Equals(category.Name)).FirstOrDefault() != null)
            {
                throw new IsExist(category.Name + " already exists in the database");
            }
            category.CreatedDate = DateTime.Now;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public string Delete(int id)
        {
            //Ver1:
            var category = this.GetById(id);
            if (category == null)
                throw new IsNotExist("There is no Category with Id is " + id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return "Delete successfully";
            //Ver2:
            //var category = this.GetById(id);
            //if (category == null)
            //    throw new IsNotExist("There is no Category with Id is " + id);
            //category.Del = true;
            //_context.SaveChanges();
            //return "Delete successfully";
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.OrderByDescending(x => x.Id).ToList();
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
                throw new IsNotExist("There is no Brand with Id is " + id);
            return category;
        }

        public Category Update(int id, Category category)
        {
            var _category = this.GetById(id);
            if (! string.Equals(_category.Name, category.Name))
            {
                if (_context.Categories.Where(x => x.Name.Equals(category.Name)).FirstOrDefault() != null)
                {
                    throw new IsExist("\" " + category.Name + " \" already exists in the database");
                }
            }
            _category.ModifiedDate = DateTime.Now;
            _category.Name = category.Name;
            _category.Description = category.Description;
            _category.IsActive = category.IsActive;
            _context.SaveChanges();
            return _category;
        }
    }
}
