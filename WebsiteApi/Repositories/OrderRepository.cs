using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteApi.Model.Entity;
using WebsiteApi.Repositories.IRepositories;

namespace WebsiteApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ApiContext _context;
        public OrderRepository(IProductRepository productRepository, ApiContext context, IUserRepository userRepository, ICartRepository cartRepository, IPaymentRepository paymentRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _paymentRepository = paymentRepository;
        }
        public string CreateOrder(List<int> listId, int IdUser, decimal Total, string Payment)
        {
            try
            {
                int IdPayment = Convert.ToInt32(_context.Payments.Where(x => x.Name.Equals(Payment)).FirstOrDefault().Id);
                var user = _userRepository.GetById(IdUser);
                Order order = new Order()
                {
                    CreatedBy = user.UserName,
                    CreatedDate = DateTime.Now,
                    Del = false,
                    IsDelivery = false,
                    ModifiedBy = string.Empty,
                    ModifiedDate = DateTime.Now,
                    PaymentId = IdPayment,
                    Total = Total, //Total : tổng tiền
                    UserId = IdUser
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                foreach (var item in listId)
                {
                    var temp = _cartRepository.GetById(item);
                    var price_temp = _context.Products.Where(p => p.Id == temp.ProductId).Select(x => x.Product_Price).FirstOrDefault();
                    var modified_temp = _context.Products.Where(p => p.Id == temp.ProductId).Select(x => x.ModifiedDate).FirstOrDefault();
                    if (modified_temp != null)
                    {
                        if (modified_temp > DateTime.Now )
                        {
                            price_temp = _context.Products.Where(p => p.Id == temp.ProductId).Select(x => x.Del_Price).FirstOrDefault();
                        }
                    }
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        ModifiedBy = string.Empty,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = user.UserName,
                        CreatedDate = DateTime.Now,
                        Del = false,
                        Message = String.Empty,
                        OrderId = order.Id,
                        ProductId = temp.ProductId,
                        Product_Price = price_temp
                    };
                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();
                }
                return "Đặt thành công sản phẩm";
            }
            catch (Exception)
            {
                throw new Exception("Vui lòng kiểm tra lại thông tin");
            }
        }

    }
}
