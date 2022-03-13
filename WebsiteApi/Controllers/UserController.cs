using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Authorize("ADMIN")]
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

        [HttpPut("Update/{id}")]
        public ActionResult<int> Update(int id, [FromBody] UserDto value)
        {
            try
            {
                return Ok(_userService.Update(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
