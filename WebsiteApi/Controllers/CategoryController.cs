using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            try
            {
                return Ok(_categoryService.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById(int id)
        {
            try
            {
                return Ok(_categoryService.GetById(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult<CategoryDto> Post([FromBody] CategoryDto value)
        {
            try
            {
                return Ok(_categoryService.Create(value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public ActionResult<CategoryDto> Update(int id, [FromBody] CategoryDto value)
        {
            try
            {
                return Ok(_categoryService.Update(id, value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_categoryService.Delete(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
