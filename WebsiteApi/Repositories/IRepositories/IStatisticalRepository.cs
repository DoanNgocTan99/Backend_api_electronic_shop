using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IStatisticalRepository
    {
        IEnumerable<StatisticalDto> GetAll();
        IEnumerable<TopCustomerDto> GetTopCustomers();
        IEnumerable<TopCustomerDto> GetFullTopCustomers();
        IEnumerable<LatestOrder> GetLatestOrders();
        IEnumerable<LatestOrder> GetFullLatestOrders();

    }
}
