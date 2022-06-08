using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;
namespace WebsiteApi.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Lấy tất cả phương thức thanh toán trong csdl
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<PaymentDto>> Get()
        {
            try
            {
                return Ok(_paymentService.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy phương thức thanh toán theo Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<PaymentDto> GetById(int id)
        {
            try
            {
                return Ok(_paymentService.GetById(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Thêm mới phương thức thanh toán (Được thực hiện dưới quyền ADMIN)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPost("Create")]
        public ActionResult<PaymentDto> Create([FromBody] PaymentDto value)
        {
            try
            {
                return Ok(_paymentService.Create(value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Chỉnh sửa phương thức thanh toán (ĐƯợc thực hiện dưới quyền ADMIN)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPut("Update/{id}")]
        public ActionResult<PaymentDto> Update(int id, [FromBody] PaymentDto value)
        {
            try
            {
                return Ok(_paymentService.Update(id, value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa phương thức thanh toán (Được thực hiện dưới quyền ADMIN)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_paymentService.Delete(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
