using System;

namespace WebsiteApi.Model.Dtos
{
    public class LatestOrder
    {
        public int OrderId { get; set; }
        public string UserName{ get; set; }
        public DateTime Date{ get; set; }
        public Decimal TotalPrice{ get; set; }
        public string Status { get; set; }

    }
}
