using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebsiteApi.Model.Entity
{
    [Table("TrackingOrder")]
    public class TrackingOrder
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public long  OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
