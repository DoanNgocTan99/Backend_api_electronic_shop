using System;

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

        public bool Del { get; set; }
    }
}
