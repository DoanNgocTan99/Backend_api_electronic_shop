using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebsiteApi.Repositories.IRepositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebsiteApi.Controllers
{

    public class RoleController : BaseApiController
    {
        private IRoleRepository _roleRepository;
        private IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var temp = _roleRepository.GetAll();
                return Ok(temp);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
