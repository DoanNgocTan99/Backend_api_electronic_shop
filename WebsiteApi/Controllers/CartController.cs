using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    
    public class CartController : BaseApiController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetListCartByIdUser/{id}")]
        public ActionResult<IEnumerable<CartDto>> GetALl(int id)
        {
            try
            {
                return Ok(_cartService.GetCarts(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFullListCartByIdUser/{id}")]
        public ActionResult<IEnumerable<Cart>> GetFullListCartByIdUser(int id)
        {
            try
            {
                return Ok(_cartService.GetFullCart(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCountProductByIdUser/{id}")]
        public ActionResult<int> GetCountProductByIdUser(int id)
        {
            try
            {
                return Ok(_cartService.GetCountProductInCart(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public ActionResult<string> Create(CartDto cart)
        {
            try
            {
                return Ok(_cartService.Create(cart));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult<string> Update(int id,[FromBody] CartDto cart)
        {
            try
            {
                return Ok(_cartService.Update(id, cart));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_cartService.Delete(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
