using WebsiteApi.Model.Entity;

namespace WebsiteApi.Services.IServices
{
    public interface ITokenService
    {
        string CreateToken(User user);
        //string CreateToken(Employee user);
    }
}
