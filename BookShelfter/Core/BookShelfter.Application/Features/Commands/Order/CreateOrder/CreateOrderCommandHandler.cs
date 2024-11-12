using BookShelfter.Application.Repositories.Customer;
using BookShelfter.Application.Repositories.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities.Enums;

namespace BookShelfter.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly ICustomerReadRepository _customerReadRepository;


        public CreateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, ICustomerReadRepository customerReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _customerReadRepository = customerReadRepository;
        }

        private string GenerateOrderCode()
        {
            return $"ORD-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }


        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
            {
                return new()
                {
                    Success = false,
                    Message = "Customer not found"
                };
            }


            var order = new Domain.Entities.Order
            {
                OrderNumber = Guid.NewGuid().ToString(),
                OrderDate = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                DeliveryAddress = request.DeliveryAddress,
                PhoneNumber = request.PhoneNumber,
                PaymentMethod = request.PaymentMethod,
                OrderCode = GenerateOrderCode(),
                Status = OrderStatus.Pending,
                OrderDetails = request.OrderDetails.Select(detail => new Domain.Entities.OrderDetails
                {
                    BookId = detail.BookId,
                    Quantity = detail.Quantity,
                    Price = detail.Price

                }).ToList()

            };
            
            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();

            return new()
            {
                Success = true,
                Message = "Order Created successffuly"
            };



        }
    }
}
