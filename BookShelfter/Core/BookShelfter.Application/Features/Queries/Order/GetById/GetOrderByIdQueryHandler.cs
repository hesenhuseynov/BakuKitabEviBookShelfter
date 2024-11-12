using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Order;
using BookShelfter.Application.Repositories.Order;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Order.GetById
{
    public  class GetOrderByIdQueryHandler:IRequestHandler<GetOrderByIdQueryRequest,GetOrderByIdQueryResponse>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order =  await _orderReadRepository.GetByIdAsync(request.OrderId);
            if (order ==null)
            {
                return new()
                {
                    Success = false,
                    Message = "Order not found"
                };


            }

            return new()
            {
                Success = true,
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(detail => new OrderDetailsDTO()
                {
                    BookId = detail.BookId,
                    Quantity = detail.Quantity,
                    Price = detail.Price
                }).ToList()
            };


        }
    }
}
