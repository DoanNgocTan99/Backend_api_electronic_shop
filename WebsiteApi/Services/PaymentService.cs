using System.Collections.Generic;
using WebsiteApi.CusException;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;
using AutoMapper;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebsiteApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        public PaymentDto Create(PaymentDto payment)
        {
            return _mapper.Map<PaymentDto>(_paymentRepository.Create(_mapper.Map<Payment>(payment)));
        }

        public string Delete(int id)
        {
            return _paymentRepository.Delete(id);
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            return _mapper.Map<IEnumerable<PaymentDto>>(_paymentRepository.GetAll());
        }

        public PaymentDto GetById(int id)
        {
            return _mapper.Map<PaymentDto>(_paymentRepository.GetById(id));
        }

        public PaymentDto Update(int id, PaymentDto payment)
        {
            return _mapper.Map<PaymentDto>(
                _paymentRepository.Update(
                    id, _mapper.Map<Payment>(payment)));
        }

        public string UploadImage(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
