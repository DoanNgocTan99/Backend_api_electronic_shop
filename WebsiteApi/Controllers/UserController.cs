using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get toàn bộ USER trong hệ thống
        /// </summary>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (System.Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status204NoContent };
            }
        }

        /// <summary>
        /// Get user theo Id
        /// </summary>
        /// <param name="id">ID user</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch (System.Exception ex)
            {
                //return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status204NoContent };
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Xóa User khỏi csdl
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpDelete("Delete/{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_userService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Chỉnh sửa user theo ID 
        /// </summary>
        /// <param name="id">Id user</param>
        /// <param name="value">giá trị mới</param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public ActionResult<int> Update(int id, [FromForm] UserDto value)
        {
            try
            {
                if (value.ImageFile != null)
                {
                    var temp = this.SaveImage(value.ImageFile);
                    value.ImagePath = _userService.UploadImage(temp);
                }
                else
                {
                    if (!string.IsNullOrEmpty(value.ImagePath) || !string.IsNullOrWhiteSpace(value.ImagePath))
                    {
                        value.ImagePath = _userService.UploadImage(value.ImagePath);
                    }
                }
                return Ok(_userService.Update(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Thay đổi pass của User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword/{id}")]
        public ActionResult<string> ChangePassword(int id, [FromBody] string password)
        {
            try
            {
                return Ok(_userService.ChangePassword(id, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
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
