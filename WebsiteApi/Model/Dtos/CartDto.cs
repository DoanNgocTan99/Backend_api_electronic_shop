using System;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Model.Dtos
{
    public class CartDto
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public long UserId { get; set; }

        public long ProductId { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string Origin { get; set; }

        public decimal Product_Price { get; set; }

        public decimal Del_Price { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public int? Stock { get; set; }

        public int? Discount { get; set; }

        public int? Views { get; set; }

        public int? Rate { get; set; }

        public bool? IsActive { get; set; }

        public bool Del { get; set; }

        public long? BrandId { get; set; }

        public long CategoryId { get; set; }
        public string Path { get; set; }

        public string Brand { get; set; }
        public string CategoryName { get; set; }
    }
}
