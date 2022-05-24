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
            var productdto = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAll());
            foreach (var item in productdto)
            {
                item.Brand = _productRepository.GetBrand(Convert.ToInt32(item.BrandId));
                item.CategoryName = _productRepository.GetCategory(Convert.ToInt32(item.CategoryId));
            }
            return productdto;
        }

        public IEnumerable<ProductDto> GetAllByCategory(string categoryName)
        {
            List<ProductDto> pro = new List<ProductDto>();
            var productdto = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAll());
            foreach (var item in productdto)
            {
                item.Brand = _productRepository.GetBrand(Convert.ToInt32(item.BrandId));
                item.CategoryName = _productRepository.GetCategory(Convert.ToInt32(item.CategoryId));
            }
            int i = 0;
            foreach (var item in productdto)
            {
                if (item.CategoryName.Contains(categoryName) && i < 10 )
                {
                    pro.Add(item);
                    i++;
                }
            }
            return pro;
        }

        public IEnumerable<ProductDto> GetRandom()
        {
            List<ProductDto> pro = new List<ProductDto>();
            var productdto = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAll());
            foreach (var item in productdto)
            {
                item.Brand = _productRepository.GetBrand(Convert.ToInt32(item.BrandId));
                item.CategoryName = _productRepository.GetCategory(Convert.ToInt32(item.CategoryId));
            }
            int i = 0;
            foreach (var item in productdto)
            {
                if ( i < 5)
                {
                    pro.Add(item);
                    i++;
                }
            }
            return pro;
        }
        public ProductDto GetById(int id)
        {
            var temp = _mapper.Map<ProductDto>(_productRepository.GetById(id));
            temp.Brand = _productRepository.GetBrand(Convert.ToInt32(temp.BrandId));
            temp.CategoryName = _productRepository.GetCategory(Convert.ToInt32(temp.CategoryId));
            return temp;
        }

        public ProductDto Update(int id, ProductDto product)
        {
            return _mapper.Map<ProductDto>(
                _productRepository.Update(
                    id, _mapper.Map<Product>(product)));
        }

        public IEnumerable<ProductDto> GetListProductByCategory(ProductListByCategory value)
        {
            List<ProductDto> pro = new List<ProductDto>();
            var productdto = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAll());
            foreach (var item in productdto)
            {
                item.Brand = _productRepository.GetBrand(Convert.ToInt32(item.BrandId));
                item.CategoryName = _productRepository.GetCategory(Convert.ToInt32(item.CategoryId));
            }
            int i = 0;
            foreach (var item in productdto)
            {
                if (item.CategoryName.Contains(value.CategoryName) && i < 10 && item.Id != value.IdProduct)
                {
                    pro.Add(item);
                    i++;
                }
            }
            return pro;
        }
    }
}
