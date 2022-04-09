using WebsiteApi.Model.Dtos;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using WebsiteApi.Model.Entity;
using System.Collections.Generic;

namespace WebsiteApi.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        public ProductImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        public string CreatePath(ProductImageDto value)
        {
            return _imageRepository.CreatePath(_mapper.Map<ProductImage>(value));
        }
    }
}
