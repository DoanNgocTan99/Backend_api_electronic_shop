using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public long Id { get; set; }

        public long OrderIdDetail { get; set; }

        public long ProdIdDetail { get; set; }

        public long PaymentIdDetail { get; set; }

        public long UserIdDetail { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Del { get; set; }

        public virtual Order Order { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
