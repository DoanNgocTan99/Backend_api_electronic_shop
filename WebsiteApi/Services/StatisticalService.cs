using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;

namespace WebsiteApi.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IStatisticalRepository _statisticalRepository;
        public StatisticalService(IStatisticalRepository statisticalRepository)
        {
            _statisticalRepository = statisticalRepository;
        }
        public IEnumerable<StatisticalDto> GetAll()
        {
            return _statisticalRepository.GetAll();
        }

        public IEnumerable<LatestOrder> GetLatestOrders()
        {
            return _statisticalRepository.GetLatestOrders();
        }
        public IEnumerable<LatestOrder> GetFullLatestOrders()
        {
            return _statisticalRepository.GetFullLatestOrders();
        }
        public IEnumerable<TopCustomerDto> GetTopCustomers()
        {
            return _statisticalRepository.GetTopCustomers();
        }

        public IEnumerable<TopCustomerDto> GetFullTopCustomers()
        {
            return _statisticalRepository.GetFullTopCustomers();
        }

        public IEnumerable<TopCustomerDto> GetTopCustomersByDate(DateExportExcel value)
        {
            return _statisticalRepository.GetTopCustomersByDate(value);
        }

        public IEnumerable<LatestOrder> GetLatestOrdersByDate(DateExportExcel value)
        {
            return _statisticalRepository.GetLatestOrdersByDate(value);
        }
    }
}
