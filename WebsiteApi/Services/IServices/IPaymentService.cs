using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> GetAll();
        PaymentDto GetById(int id);
        PaymentDto Create(PaymentDto payment);
        PaymentDto Update(int id, PaymentDto payment);
        string Delete(int id);
        string UploadImage(string path);
    }
}
