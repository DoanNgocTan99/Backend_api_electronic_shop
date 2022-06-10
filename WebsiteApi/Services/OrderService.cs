using System.Collections.Generic;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public string CreateOrder(List<int> listId, int IdUser, decimal Total, string Payment)
        {
            return _orderRepository.CreateOrder(listId, IdUser, Total, Payment);
        }
    }
}
