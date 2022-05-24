using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.CusException;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApiContext _context;

        public PaymentRepository(ApiContext context)
        {
            _context = context;
        }
        public Payment Create(Payment payment)
        {
            if (_context.Payments.Where(x => x.Name.Equals(payment.Name)).FirstOrDefault() != null)
            {
                throw new IsExist(payment.Name + " already exists in the database");
            }
            payment.CreatedDate = DateTime.Now;
            payment.CreatedBy = "ADMIN";
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public string Delete(int id)
        {
            var payment = this.GetById(id);
            if (payment == null)
                throw new IsNotExist("There is no Category with Id is " + id);
            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return "Delete successfully";
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.OrderByDescending(x => x.Id).ToList();
        }

        public Payment GetById(int id)
        {
            var payment = _context.Payments.Where(x => x.Id == id).FirstOrDefault();
            if (payment == null)
                throw new IsNotExist("There is no Brand with Id is " + id);
            return payment;
        }

        public Payment Update(int id, Payment payment)
        {
            var _payment = this.GetById(id);
            if (!string.Equals(_payment.Name, _payment.Name))
            {
                if (_context.Categories.Where(x => x.Name.Equals(_payment.Name)).FirstOrDefault() != null)
                {
                    throw new IsExist("\" " + _payment.Name + " \" already exists in the database");
                }
            }
            _payment.Name = payment.Name;
            _payment.ModifiedDate = DateTime.Now;
            _payment.ModifiedBy = "ADMIN";
            _payment.Type = payment.Type;
            _payment.Del = false;
            _payment.Allow = true;
            _context.SaveChanges();
            return _payment;
        }
    }
}
