using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
namespace WebsiteApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApiContext _context;
        public CartRepository(ApiContext context)
        {
            _context = context;
        }
        public string Create(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return "Create successful";
        }

        public string Delete(int id)
        {
            if (this.GetById(id) == null)
                throw new IsNotExist("");
            _context.Carts.Remove(this.GetById(id));
            return "Delete successful";
        }

        public Cart GetById(int id)
        {
            return _context.Carts.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Cart> GetCarts(int UserId)
        {
            var listCart = _context.Carts.Where(c => c.UserId == UserId).ToList();
            return listCart;
        }

        public string Update(int id, Cart cart)
        {
            var cartItem = this.GetById(id);
            if (cartItem != null)
            {
                throw new IsNotExist("There are no products in the cart");
            }
            cartItem.ProductId = cart.ProductId;
            cartItem.Count = cart.Count;
            cart.ModifiedDate = DateTime.Now;
            _context.SaveChanges();
            return "Update successful";
        }
    }
}
