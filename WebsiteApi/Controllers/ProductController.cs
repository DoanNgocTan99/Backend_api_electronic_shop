using WebsiteApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        public ProductController(IProductService productService, IProductImageService productImageService)
        {
            _productService = productService;
            _productImageService = productImageService;
        }

        /// <summary>
        /// Lấy toàn bộ sản phẩm có trong csdl
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            try
            {
                return Ok(_productService.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy toàn bộ sản phẩm có trong csdl theo Id sản phẩm
        /// </summary>
        /// <param name="id"> Id sản phẩm </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            try
            {
                return Ok(_productService.GetById(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy sản phẩm theo tên danh mục
        /// </summary>
        /// <param name="categoryname">Tên danh mục sản phẩm</param>
        /// <returns></returns>
        [HttpGet("roductRelated/{categoryname}")]
        public ActionResult<ProductDto> GetByCategoryName(string categoryname)
        {
            try
            {
                return Ok(_productService.GetAllByCategory(categoryname));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy sản phẩm theo tên danh sách danh mục
        /// </summary>
        /// <param name="value">List danh mục sản phẩm</param>
        /// <returns></returns>
        [HttpPost("ProductRelated")]
        public ActionResult<ProductDto> GetProductsByCategoryName(ProductListByCategory value)
        {
            try
            {
                return Ok(_productService.GetListProductByCategory(value));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tạo mới sản phẩm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPost("Create")]
        public ActionResult<ProductDto> Create([FromBody] ProductDto value)
        {
            try
            {
                var imagePathLater = string.Empty;
                var temp = _productService.Create(value);
                if (value.ImageFile != null)
                {
                    var imagePath = this.SaveImage(value.ImageFile);
                    imagePathLater = _productImageService.UploadImage(imagePath);
                }
                else
                {
                    imagePathLater = _productImageService.UploadImage(value.Avt);
                }
                ProductImageDto pro = new ProductImageDto()
                {
                    ProductId = temp.Id,
                    Path = imagePathLater
                };
                //temp.Path = _productImageService.CreatePath(pro);
                return Ok(temp);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Chỉnh sửa sản phẩm. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPut("Update/{id}")]
        public ActionResult<ProductDto> Update(int id, [FromBody] ProductDto value)
        {
            try
            {
                var imagePathLater = string.Empty;
                var temp = _productService.Update(id, value);
                if (value.ImageFile != null)
                {
                    var imagePath = this.SaveImage(value.ImageFile);
                    imagePathLater = _productImageService.UploadImage(imagePath);
                }
                else
                {
                    if (string.IsNullOrEmpty(value.Avt) || string.IsNullOrWhiteSpace(value.Avt))
                    {
                        imagePathLater = _productImageService.UploadImage(value.Avt);
                    }
                }

                ProductImageDto pro = new ProductImageDto()
                {
                    ProductId = temp.Id,
                    Path = imagePathLater
                };
                //temp.Path = _productImageService.CreatePath(pro);
                return Ok(temp);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Xóa sản phẩm khỏi csdl
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_productService.Delete(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy đường dẫn ảnh để lưu vào csdl
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
    }
}
