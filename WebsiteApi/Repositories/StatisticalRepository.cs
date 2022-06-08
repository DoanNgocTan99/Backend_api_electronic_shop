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

        public IEnumerable<LatestOrder> GetLatestOrders()
        {
            int i = 1;
            List<LatestOrder> result = new List<LatestOrder>();
            var ListUserOrder = _context.Orders.ToList().OrderByDescending(x => x.CreatedDate);
            foreach (var item in ListUserOrder)
            {
                if (i < 6)
                {
                    var user = new LatestOrder();
                    var listUser = _context.Users.Where(x => x.Id == item.UserId).ToArray();
                    if (listUser != null)
                    {
                        user.UserName = listUser[0].UserName;
                    }
                    else
                        user.UserName = string.Empty;

                    user.OrderId = Convert.ToInt32(item.Id);
                    var order = _context.Orders.Where(x => x.Id == item.Id).FirstOrDefault();
                    user.TotalPrice = Convert.ToDecimal(order.Total);
                    user.Date = Convert.ToDateTime(order.CreatedDate);
                    user.Status = _context.TrackingOrders.Where(x => x.OrderId == item.Id).Select(x => x.Status).FirstOrDefault();
                    if (user.Status == null)
                    {
                        user.Status = "Unknown";
                    }
                    result.Add(user);
                }
                i++;
            }
            return result;

        }

        public IEnumerable<LatestOrder> GetFullLatestOrders()
        {
            List<LatestOrder> result = new List<LatestOrder>();
            var ListUserOrder = _context.Orders.ToList().OrderByDescending(x => x.CreatedDate);
            foreach (var item in ListUserOrder)
            {
                var user = new LatestOrder();
                var listUser = _context.Users.Where(x => x.Id == item.UserId).ToArray();
                if (listUser != null)
                {
                    user.UserName = listUser[0].UserName;
                }
                else
                    user.UserName = string.Empty;

                user.OrderId = Convert.ToInt32(item.Id);
                var order = _context.Orders.Where(x => x.Id == item.Id).FirstOrDefault();
                user.TotalPrice = Convert.ToDecimal(order.Total);
                user.Date = Convert.ToDateTime(order.CreatedDate);
                user.Status = _context.TrackingOrders.Where(x => x.OrderId == item.Id).Select(x => x.Status).FirstOrDefault();
                if (user.Status == null)
                {
                    user.Status = "Unknown";
                }
                result.Add(user);
            }
            return result;

        }

        public IEnumerable<TopCustomerDto> GetTopCustomers()
        {
            int i = 0;
            List<TopCustomerDto> result = new List<TopCustomerDto>();
            var ListUserOrder = _context.Orders.Select(x => x.UserId).Distinct();
            foreach (var item in ListUserOrder)
            {
                if (i < 6)
                {
                    var user = new TopCustomerDto();
                    var listUser = _context.Users.Where(x => x.Id == item).ToArray();
                    if (listUser != null)
                    {
                        user.UserName = listUser[0].UserName;
                    }
                    else
                        continue;
                    var totalSpendings = _context.Orders.Where(x => x.UserId == item).ToList();
                    foreach (var value in totalSpendings)
                    {
                        user.TotalSpending += Convert.ToDecimal(value.Total);
                    }
                    user.TotalOrders = totalSpendings.Count;

                    result.Add(user);
                }
                i++;
            }
            return result;

        }

        public IEnumerable<TopCustomerDto> GetFullTopCustomers()
        {
            List<TopCustomerDto> result = new List<TopCustomerDto>();
            var ListUserOrder = _context.Orders.Select(x => x.UserId).Distinct();
            foreach (var item in ListUserOrder)
            {
                var user = new TopCustomerDto();
                var listUser = _context.Users.Where(x => x.Id == item).ToArray();
                if (listUser != null)
                {
                    user.UserName = listUser[0].UserName;
                }
                else
                    continue;
                var totalSpendings = _context.Orders.Where(x => x.UserId == item).ToList();
                foreach (var value in totalSpendings)
                {
                    user.TotalSpending += Convert.ToDecimal(value.Total);
                }
                user.TotalOrders = totalSpendings.Count;

                result.Add(user);
            }
            return result;
        }
    }
}
