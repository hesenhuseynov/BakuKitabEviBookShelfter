using BookShelfter.Application.Repositories.Order;
using BookShelfter.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Order.UpdateOrder
{
    public  class UpdateOrderCommandHandler:IRequestHandler<UpdateOrderCommandRequest,UpdateOrderCommandResponse>
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public UpdateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async  Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new UpdateOrderCommandResponse
                {
                    Success = false,
                    Message = "Order not found"
                };
            }
            order.OrderDetails = request.OrderDetails.Select(detail => new OrderDetails
            {
                BookId = detail.BookId,
                Quantity = detail.Quantity,
                Price = detail.Price
            }).ToList();
            _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();

            return new UpdateOrderCommandResponse
            {
                Success = true,
                Message = "Order updated successfully"
            };



        }
    }
}
