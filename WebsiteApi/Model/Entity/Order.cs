using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApi.Model.Entity
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            TrackingOrders = new HashSet<TrackingOrder>();
        }

        [Key]
        public long Id { get; set; }

        public decimal? Total { get; set; }

        public bool? IsDelivery { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Del { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public long PaymentId { get; set; }
        public virtual Payment Payment { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<TrackingOrder> TrackingOrders { get; set; }
    }
}
