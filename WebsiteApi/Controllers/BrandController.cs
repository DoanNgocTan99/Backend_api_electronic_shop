using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;


namespace WebsiteApi.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Lấy toàn bộ brand có trong csdl 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy Brand theo id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tạo mới brand dưới quyền ADMIN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
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

        /// <summary>
        /// Chỉnh sửa Brand dưới quyền ADMIN
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
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

        /// <summary>
        /// Xóa brand hiện có theo id của Brand được trả về từ Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
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
