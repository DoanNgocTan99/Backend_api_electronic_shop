using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(250)]
        public string Amount { get; set; }

        [Required]
        [StringLength(250)]
        public string Status { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long UserIdTransaction { get; set; }

        public long ProdIdTransaction { get; set; }

        public bool Del { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
