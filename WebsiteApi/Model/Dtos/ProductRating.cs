using Microsoft.ML.Data;

namespace WebsiteApi.Model.Dtos
{
    public class ProductRating
    {
        [LoadColumn(0)]
        public float userId;
        [LoadColumn(1)]
        public float productId;
        [LoadColumn(2)]
        public float Label;
    }
}
