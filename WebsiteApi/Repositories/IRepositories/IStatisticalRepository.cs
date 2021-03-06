using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IStatisticalRepository
    {
        IEnumerable<StatisticalDto> GetAll();
        IEnumerable<TopCustomerDto> GetTopCustomers();
        IEnumerable<LatestOrder> GetLatestOrders();
        IEnumerable<TopCustomerDto> GetFullTopCustomers();
        IEnumerable<LatestOrder> GetFullLatestOrders();
        IEnumerable<TopCustomerDto> GetTopCustomersByDate(DateExportExcel value);
        IEnumerable<LatestOrder> GetLatestOrdersByDate(DateExportExcel value);

    }
}
