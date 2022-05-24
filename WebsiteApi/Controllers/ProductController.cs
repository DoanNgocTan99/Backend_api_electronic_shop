using Microsoft.AspNetCore.Authorization;
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

        //[HttpGet("ProductRelated/")]
        //public ActionResult<ProductDto> GetByCategoryName()
        //{
        //    try
        //    {
        //        return Ok(_productService.GetRandom());
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
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
