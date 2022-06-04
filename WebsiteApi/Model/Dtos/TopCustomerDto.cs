namespace WebsiteApi.Model.Dtos
{
    public class TopCustomerDto
    {
        public string UserName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpending { get; set; }
    }
}
