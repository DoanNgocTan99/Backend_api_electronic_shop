using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
namespace WebsiteApi.Services.IServices
{
    public interface ICartService
    {
        IEnumerable<CartDto> GetCarts(int UserId);
        CartDto GetById(int id);
        string Create(CartDto cart);
        string Update(int id, CartDto cart);
        string Delete(int id);
    }
}
