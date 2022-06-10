using AutoMapper;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
namespace WebsiteApi.Mapper
{
    public class PaymentMapping: Profile
    {
        public PaymentMapping()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();
        }
    }
}
