using BookShelfter.Application.Repositories.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Order;

namespace BookShelfter.Application.Features.Queries.Order.GetOrderWithDetailsById
{
    public  class GetOrderWithDetailsByIdQueryHandler:IRequestHandler<GetOrderWithDetailsByIdQueryRequest,GetOrderWithDetailsByIdQueryResponse>
    {
        private readonly IOrderReadRepository _repository;

        public GetOrderWithDetailsByIdQueryHandler(IOrderReadRepository repository)
        {
            _repository = repository;
        }

        public async  Task<GetOrderWithDetailsByIdQueryResponse> Handle(GetOrderWithDetailsByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result  = await _repository.GetOrderWithDetailsByIdAsync(request.OrderId);
             

            if (result == null)
            {
                return new()
                {
                    Message = "not found this order any details",
                    Success = false

                }; 
            }

            return new()
            {
                Success = true,
                Message = "Details Order retrieved succesffuly",
                OrderId = result.Id,
                OrderNumber = result.OrderNumber,
                OrderDate = result.OrderDate,
                DeliveryAddress = result.DeliveryAddress,
                CustomerId=result.CustomerId,
                CustomerPhoneNumber = result.PhoneNumber,
                PaymentMethod=result.PaymentMethod,
                OrderDetails = result.OrderDetails.Select(od => new OrderDetailsDTO
                {
                    BookId = od.BookId,
                    BookName = od.Book.BookName,
                    BookImageUrl = od.Book.BookImagesFile?.Select(c=>c.ImageUrl).FirstOrDefault(),
                    Price = od.Price,
                    Quantity = od.Quantity

                }).ToList()
            };







        }

         
    }
}
