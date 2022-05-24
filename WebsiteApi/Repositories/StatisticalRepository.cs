using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class StatisticalRepository : IStatisticalRepository
    {
        private readonly ApiContext _context;
        public StatisticalRepository(ApiContext context)
        {
            _context = context;
        }
        public IEnumerable<StatisticalDto> GetAll()
        {
            List<StatisticalDto> result = new List<StatisticalDto>();
            decimal countVisits = 0;
            decimal countPrices = 0;

            var ViewCount = _context.Products.Select(x => x.Views).ToList();
            var TotalPrices = _context.Products.Select(x => x.Product_Price).ToList();
            var TotalOrders = _context.Orders.ToList();
            foreach (var item in TotalPrices)
            {
                countPrices += Convert.ToInt32(item);
            }
            foreach (var item in ViewCount)
            {
                if (item != null)
                    countVisits += Convert.ToInt32(item);
            }
            StatisticalDto TotalVisits = new StatisticalDto()
            {
                Icon = "fas fa-shopping-cart",
                Count = countVisits,
                Title = "Total visits"
            };
            StatisticalDto TotalSales = new StatisticalDto()
            {
                Icon = "fas fa-shopping-bag",
                Count = Convert.ToDecimal(1990000),
                Title = "Total sales"
            };
            StatisticalDto TotalPrice = new StatisticalDto()
            {
                Icon = "fas fa-dollar-sign",
                Count = countPrices,
                Title = "Total income"
            };
            StatisticalDto TotalOrder = new StatisticalDto()
            {
                Icon = "fas fa-receipt",
                Count = Convert.ToDecimal(TotalOrders.Count),
                Title = "Total orders"
            };
            result.Add(TotalVisits);
            result.Add(TotalPrice);
            result.Add(TotalOrder);
            result.Add(TotalSales);

            return result;
        }
    }
}
