using System.Collections.Generic;
using WebsiteApi.Model.Entity;

namespace WebsiteApi.Repositories.IRepositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        Payment Create(Payment payment);
        Payment Update(int id, Payment payment);
        string Delete(int id);
    }
}
