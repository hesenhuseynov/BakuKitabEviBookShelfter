using BookShelfter.Application.DTOs.Customer;
using BookShelfter.Application.Repositories.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Queries.Customer
{
    public  class GetCustomerByAppUserIdQueryHandler : IRequestHandler<GetCustomerByAppUserIdQueryRequest,GetCustomerByAppUserIdQueryResponse>
    {
        private readonly ICustomerReadRepository _readRepository;

        public GetCustomerByAppUserIdQueryHandler(ICustomerReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public  async Task<GetCustomerByAppUserIdQueryResponse> Handle(GetCustomerByAppUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var customer = await _readRepository.GetCustomerByAppUserIdAsync(request.UserId);
            if (customer == null)
            {
                return new GetCustomerByAppUserIdQueryResponse()
                {
                    Success = false,
                    Message = "Customer Not FOund "
                };
               
            }


            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };



            return new()
            {
                Success = true,
                Message = "Customer retrieved successfully",
                Customer = customerDto
            };

      
        }
    }
}
