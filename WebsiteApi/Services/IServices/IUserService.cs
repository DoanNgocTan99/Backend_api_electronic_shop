using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services.IServices
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        string Delete(int id);
        bool check(string Email = "", string UserName = "", string Phone = "");
        UserDto Update(int id, UserDto user);

    }
}
