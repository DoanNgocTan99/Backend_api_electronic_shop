using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface IRatingService
    {
        void Recommend(List<ProductRating> value);
    }
}
