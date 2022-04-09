using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApi.Model.Dtos
{
    public class ProductImageDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        public string Label { get; set; }

        public long ProductId { get; set; }
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IdDel { get; set; }
    }
}
