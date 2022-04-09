using System;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApiContext _context;
        public ImageRepository(ApiContext context)
        {
            _context = context;
        }

        public string CreatePath(ProductImage value)
        {
            Random random = new Random();
            var rd = random.Next(1, 1000);
            _context.ProductImages.Add(value);
            _context.SaveChanges();
            return value.Path;
        }
    }
}
