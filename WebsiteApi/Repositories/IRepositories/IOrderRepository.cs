using System.Collections.Generic;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        string CreateOrder(List<int> listId, int IdUser, decimal Total, string Payment);
    }
}
