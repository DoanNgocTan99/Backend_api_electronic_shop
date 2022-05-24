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
