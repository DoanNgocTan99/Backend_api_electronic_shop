using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;

namespace WebsiteApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            this._brandRepository = brandRepository;
            this._mapper = mapper;
        }

        public BrandDto Create(BrandDto brand)
        {
            return _mapper.Map<BrandDto>(_brandRepository.Create(_mapper.Map<Brand>(brand)));
        }

        public string Delete(int id)
        {
            return _brandRepository.Delete(id);
        }

        public IEnumerable<BrandDto> GetAll()
        {
            return _mapper.Map<IEnumerable<BrandDto>>(_brandRepository.GetAll());
        }

        public BrandDto GetById(int id)
        {
            var bra = _mapper.Map<BrandDto>(_brandRepository.GetById(id));
            if (bra is null)
            {
                throw new IsNotExist("There is no user with Id is " + id);
            }
            return bra;
        }

        public BrandDto Update(int id, BrandDto brand)
        {
            if (!_brandRepository.CheckName(brand.Name))
                throw new IsExist("Brand name is \"" + brand.Name + "\" already exists in database");
            return _mapper.Map<BrandDto>(_brandRepository.Update(id, _mapper.Map<Brand>(brand)));
        }


    }
}
