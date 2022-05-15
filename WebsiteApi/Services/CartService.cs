using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;
using AutoMapper;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartService(IMapper mapper, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public string Create(CartDto cart)
        {
            return _cartRepository.Create(_mapper.Map<Cart>(cart));
        }

        public string Delete(int id)
        {
            return _cartRepository.Delete(id);
        }

        public CartDto GetById(int id)
        {
            return _mapper.Map<CartDto>(_cartRepository.GetById(id));
        }

        public IEnumerable<CartDto> GetCarts(int UserId)
        {
            List<CartDto> cartDto = new List<CartDto>();
            var listFullCart =  _cartRepository.GetCarts(UserId);
            foreach (var item in listFullCart)
            {
                cartDto.Add(new CartDto()
                {
                    Id = item.Id,
                    Count = item.Count,
                    UserId = item.UserId,
                    CreatedBy = item.CreatedBy,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate,
                    CreatedDate = item.CreatedDate,
                    Name = item.Product.Name,
                    Description = item.Product.Description,
                    Material = item.Product.Material,
                    Origin = item.Product.Origin,
                    Product_Price = item.Product.Product_Price,
                    Del_Price = item.Product.Del_Price,
                    WarrantyDate = item.Product.WarrantyDate,
                    Stock = item.Product.Stock,
                    Discount = item.Product.Discount,
                    Views = item.Product.Views,
                    Rate = item.Product.Rate,
                    IsActive = item.Product.IsActive,
                    BrandId = item.Product.BrandId,
                    CategoryId = item.Product.CategoryId,
                    Path = _productRepository.GetPath(item.ProductId),
                    Brand = _productRepository.GetBrand(item.Product.BrandId.GetValueOrDefault()),
                    CategoryName = _productRepository.GetCategory(item.Product.CategoryId),
                    ProductId = item.ProductId
                });
            }
            return cartDto;
        }

        public IEnumerable<Cart> GetFullCart(int UserId)
        {
            return _cartRepository.GetCarts(UserId);
        }
        public int GetCountProductInCart(int IdUser)
        {
            return _cartRepository.CountProductInCart(IdUser);
        }

        public string Update(int id, CartDto cart)
        {
            return _cartRepository.Update(id, _mapper.Map<Cart>(cart));
        }
    }
}
