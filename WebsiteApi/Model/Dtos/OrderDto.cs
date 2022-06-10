using System.Collections.Generic;

namespace WebsiteApi.Model.Dtos
{
    public class OrderDto
    {
        public List<int> listId { get; set; }
        public int userId { get; set; }
        public decimal Total { get; set; }
        public string Payment { get; set; }
    }
}
