using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Order;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Order.DeleteOrder
{
    public  class DeleteOrderCommandHandler:IRequestHandler<DeleteOrderCommandRequest,DeleteOrderCommandResponse>
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public DeleteOrderCommandHandler(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                return new DeleteOrderCommandResponse
                {
                    Success = false,
                    Message = "Order not found"
                };
            }



            _orderWriteRepository.Remove(order);
            await _orderWriteRepository.SaveAsync();

            return new DeleteOrderCommandResponse
            {
                Success = true,
                Message = "Order deleted successfully"
            };
        }
    }
}
