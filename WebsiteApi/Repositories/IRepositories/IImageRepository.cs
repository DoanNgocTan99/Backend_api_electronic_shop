using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IImageRepository
    {
        string CreatePath(ProductImage value);
    }
}
