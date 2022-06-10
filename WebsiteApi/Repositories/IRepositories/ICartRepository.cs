using WebsiteApi.Model.Entity;
using System.Collections.Generic;
namespace WebsiteApi.Repositories.IRepositories
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetCarts(int UserId);
        Cart GetById(int id);
        string Create(Cart cart);
        string Update(int id, Cart cart);
        string Delete(int id);
        int CountProductInCart(int idUser);
    }
}
