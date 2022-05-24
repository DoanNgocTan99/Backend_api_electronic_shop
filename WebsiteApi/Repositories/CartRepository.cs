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

        public int CountProductInCart(int idUser)
        {
            int count = 0;
            var listCart = _context.Carts.Where(x => x.UserId == idUser).ToList();
            if (listCart == null)
            {
                return 0;
            }
            foreach (var item in listCart)
            {
                count += item.Count;
            }
            return count;
        }

        public string Create(Cart cart)
        {
            var ProductInCartByUser = _context.Carts.Where(x => x.UserId == cart.UserId && x.ProductId == cart.ProductId).FirstOrDefault();
            if (ProductInCartByUser != null)
            {
                ProductInCartByUser.Count += cart.Count;
            }
            else
            {
                _context.Carts.Add(cart);
            }
            _context.SaveChanges();
            return "Create successful";
        }

        public string Delete(int id)
        {
            var carts = _context.Carts.Where(x => x.UserId == id).ToList();
            if (carts.Count == 0)
                throw new IsNotExist("");
            foreach (var item in carts)
            {
                _context.Carts.Remove(item);
            }
            _context.SaveChanges();
            return "Delete successful";
        }

        public Cart GetById(int id)
        {
            return _context.Carts.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Cart> GetCarts(int UserId)
        {
            var listCart = _context.Carts.Where(c => c.UserId == UserId).ToList();
            foreach (var item in listCart)
            {
                item.Product = _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
            }
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
