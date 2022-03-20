using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Mapper
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
