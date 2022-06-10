using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebsiteApi.Helpers;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteApi.Controllers
{

    public class RoleController : BaseApiController
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this._roleService = roleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy toàn bộ ROLE có trong hệ thống
        /// </summary>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_roleService.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update ROLE có trong hệ thống
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Authorize("ADMIN")]
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] RoleDto role)
        {
            try
            {
                return Ok(_roleService.Update(id, role));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
