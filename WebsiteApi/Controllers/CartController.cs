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

        /// <summary>
        /// Lấy Cart theo Id của User đăng nhập
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Đếm số lượng Sản phẩm trong Cart 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("CountProductInCart/{id}")]
        public ActionResult<int> CountProductInCart(int id)
        {
            try
            {
                var count = 0;
                var listCart = _cartService.GetCarts(id);
                if (listCart == null)
                {
                    return Ok(count);
                }
                foreach (var item in listCart)
                {
                    count++;
                }
                return Ok(count);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy toàn bộ giỏ hàng theo User có trong csdl
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Đếm số lượng sản phẩm trong giỏ hàng theo Id user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Thêm mới sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public ActionResult<string> Update(int id, [FromBody] CartDto cart)
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

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
