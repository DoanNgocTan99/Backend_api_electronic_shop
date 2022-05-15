using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services.IServices
{
    public interface ICartService
    {
        IEnumerable<CartDto> GetCarts(int UserId);
        IEnumerable<Cart> GetFullCart(int UserId);

        CartDto GetById(int id);
        string Create(CartDto cart);
        string Update(int id, CartDto cart);
        string Delete(int id);
        int GetCountProductInCart(int IdUser);
    }
}
