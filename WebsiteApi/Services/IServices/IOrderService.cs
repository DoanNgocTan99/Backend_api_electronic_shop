using System.Collections.Generic;

namespace WebsiteApi.Services.IServices
{
    public interface IOrderService
    {
        string CreateOrder(List<int> listId, int IdUser, decimal Total, string Payment);
    }
}
