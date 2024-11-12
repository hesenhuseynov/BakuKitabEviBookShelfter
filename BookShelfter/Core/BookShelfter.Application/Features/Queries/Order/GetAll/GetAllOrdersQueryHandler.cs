using BookShelfter.Application.DTOs.Order;
using BookShelfter.Application.Repositories.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Features.Queries.Order.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        private readonly IOrderReadRepository _repository;

        public GetAllOrdersQueryHandler(IOrderReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders =  _repository.GetAll().ToList();

            var orderDtos = orders.Select(od => new OrderDTO
            { 
              Id  = od.Id,
                Address = od.DeliveryAddress,
                OrderDate = od.OrderDate,
                OrderNumber = od.OrderNumber,
                Status = od.Status
            }).ToList();


            if (!orderDtos.Any())
            {
                return new()
                {
                    Message = "Orders not found",
                    Success = false
                };
            }


            //if (orders == null)
            //{
            //    return new()
            //    {
            //        Message = "not found orders",
            //        Success = false

            //    };
            //}


         return new()
    {
        Success = true,
        Message = "Orders retrieved successfully",
        Orders = orderDtos
    };







        }
    }
}
