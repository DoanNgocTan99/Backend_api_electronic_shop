using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebsiteApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public CategoryDto Create(CategoryDto category)
        {
            return _mapper.Map<CategoryDto>(_categoryRepository.Create(_mapper.Map<Category>(category)));
        }

        public string UploadImage(string path)
        {
            if(!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
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
        public string Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetAll());
        }

        public CategoryDto GetById(int id)
        {
            return _mapper.Map<CategoryDto>(_categoryRepository.GetById(id));
        }

        public CategoryDto Update(int id, CategoryDto category)
        {
            return _mapper.Map<CategoryDto>(
                _categoryRepository.Update(
                    id, _mapper.Map<Category>(category)));
        }
    }
}
