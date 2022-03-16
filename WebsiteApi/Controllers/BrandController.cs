using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteApi.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<BrandDto>> Get()
        {
            try
            {
                return Ok(_brandService.GetAll());
            }
            catch (System.Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status204NoContent };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<BrandDto> GetById(int id)
        {
            try
            {
                return Ok(_brandService.GetById(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult<BrandDto> Post([FromBody] BrandDto value)
        {
            try
            {
                return Ok(_brandService.Create(value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public ActionResult<BrandDto> Put(int id, [FromBody] BrandDto value)
        {
            try
            {
                return Ok(_brandService.Update(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_brandService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
