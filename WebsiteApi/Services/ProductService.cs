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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public ProductDto Create(ProductDto product)
        {
            return _mapper.Map<ProductDto>(_productRepository.Create(_mapper.Map<Product>(product)));
        }

        public string Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAll());
        }

        public ProductDto GetById(int id)
        {
            return _mapper.Map<ProductDto>(_productRepository.GetById(id));
        }

        public ProductDto Update(int id, ProductDto product)
        {
            return _mapper.Map<ProductDto>(
                _productRepository.Update(
                    id, _mapper.Map<Product>(product)));
        }
    }
}
