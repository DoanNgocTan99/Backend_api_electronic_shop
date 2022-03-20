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
