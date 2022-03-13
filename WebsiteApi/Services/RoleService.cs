using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;
using WebsiteApi.Repositories.IRepositories;
using AutoMapper;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public IEnumerable<RoleDto> GetAll()
        {
            return _mapper.Map<IEnumerable<RoleDto>>(_roleRepository.GetAll());
        }

        public RoleDto Update(int id, RoleDto role)
        {
            return _mapper.Map<RoleDto>(_roleRepository.Update(id, _mapper.Map<Role>(role)));
        }
    }
}
