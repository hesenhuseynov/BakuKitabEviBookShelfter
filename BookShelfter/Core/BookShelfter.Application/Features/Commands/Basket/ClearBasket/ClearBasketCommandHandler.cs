using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Application.Repositories.BasketItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Basket.ClearBasket
{
    public  class ClearBasketCommandHandler:IRequestHandler<ClearBasketCommandRequest,ClearBasketCommandResponse>
    {

        private readonly IBasketWriteRepository _basketWriteRepository;

        public ClearBasketCommandHandler(IBasketWriteRepository basketWriteRepository)
        {
            _basketWriteRepository = basketWriteRepository;
        }


        public async  Task<ClearBasketCommandResponse> Handle(ClearBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var result  = await  _basketWriteRepository.ClearAllBasketAsync(request.UserId);

            if (!result)
            {
                return new()
                {
                    Success = false,
                    Message = "Failed to clear the basket, or the basket was already empty."
                };
            }


            return new()
            {
                Success = true,
                Message = "Basket was successfully cleared."
            };


        }
            
            
            
        
    }
}
