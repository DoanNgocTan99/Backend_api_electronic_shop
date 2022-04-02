using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        public string Label { get; set; }

        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IdDel { get; set; }
    }
}
