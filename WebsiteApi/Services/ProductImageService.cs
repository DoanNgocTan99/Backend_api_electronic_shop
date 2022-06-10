using WebsiteApi.Model.Dtos;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using WebsiteApi.Model.Entity;
using System.Collections.Generic;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
        public string UploadImage(string path)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
            {
                string CLOUD_NAME = "tandn";
                string API_KEY = "661621979949236";
                string API_SECRET = "-QNMYjxVSCWpWhi0dLWj7G_hv_g";
                Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
                Cloudinary cloudinary = new Cloudinary(account);
                cloudinary.Api.Secure = true;
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(path)
                };
                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUri.ToString();
            }
            return string.Empty;
        }
    }
}
