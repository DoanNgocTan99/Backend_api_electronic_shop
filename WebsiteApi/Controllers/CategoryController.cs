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
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Lấy tất cả danh mục sản phẩm trong csdl
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Lấy danh mục sản phẩm theo Id sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tạo mới danh mục sản phẩm dưới quyền ADMIN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPost("Create")]
        public ActionResult<CategoryDto> Create([FromBody] CategoryDto value)
        {
            try
            {
                if (value.ImageFile != null)
                {
                    var imagePath = this.SaveImage(value.ImageFile);
                    value.ImagePath = _categoryService.UploadImage(imagePath);
                }
                else
                {
                    value.ImagePath = _categoryService.UploadImage(value.ImagePath);
                }
                return Ok(_categoryService.Create(value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy đường dẫn để thực hiện lưu Ảnh
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [NonAction]
        private string SaveImage(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);

                string extension = Path.GetExtension(imageFile.FileName);
                fileName = fileName + extension;
                string pathLocal = Directory.GetCurrentDirectory();
                string path = pathLocal + fileName;
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                return path;
            }
            return String.Empty;
        }

        /// <summary>
        /// Chỉnh sửa danh mục sản phẩm theo Id danh mục sản phẩm (Đực thực hiện dưới quyền ADMIN)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPut("Update/{id}")]
        public ActionResult<CategoryDto> Update(int id, [FromBody] CategoryDto value)
        {
            try
            {
                if (value.ImageFile != null)
                {
                    var imagePath = this.SaveImage(value.ImageFile);
                    value.ImagePath = _categoryService.UploadImage(imagePath);
                }
                else
                {
                    value.ImagePath = _categoryService.UploadImage(value.ImagePath);
                }
                return Ok(_categoryService.Update(id, value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa danh mục sản phẩm khỏi csdl (Được thực hiện dưới quyền ADMIN)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
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
