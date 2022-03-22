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
        public CartService(IMapper mapper, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
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
            return _mapper.Map<IEnumerable<CartDto>>(_cartRepository.GetCarts(UserId));
        }

        public string Update(int id, CartDto cart)
        {
            return _cartRepository.Update(id, _mapper.Map<Cart>(cart));
        }
    }
}
