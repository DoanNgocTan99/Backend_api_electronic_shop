using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiContext _context;

        public CommentRepository(ApiContext context)
        {
            _context = context;
        }
        public string Create()
        {
            try
            {
                var listIdProduct = GetAllIdProductInDatabase();
                var listIdUser = GetAllIdUserInDatabase();
                foreach (var idProduct in listIdUser)
                {
                    Comment comment = new Comment()
                    {
                        Content = string.Format("Đánh giá mang tính chất minh họa."),
                        UserId = idProduct,
                        ProductId = new Random().Next(61, 176),
                        Rate = new Random().Next(1, 6),
                        Del = false
                    };
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                }
                return "DONE";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private List<long> GetAllIdProductInDatabase()
        {
            return _context.Products.Select(x => x.Id).ToList();
        }

        private List<long> GetAllIdUserInDatabase()
        {
            return _context.Users.Select(x => x.Id).ToList();
        }
    }
}
