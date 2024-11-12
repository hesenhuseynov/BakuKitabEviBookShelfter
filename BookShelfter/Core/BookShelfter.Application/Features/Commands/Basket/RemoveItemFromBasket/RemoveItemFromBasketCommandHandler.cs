using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Application.Repositories.BasketItem;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Basket.RemoveItemFromBasket
{
    public class RemoveItemFromBasketCommandHandler : IRequestHandler<RemoveItemFromBasketCommandRequest, RemoveItemFromBasketCommandResponse>
    {
        private readonly IBasketWriteRepository _basketWriteRepository;

        public RemoveItemFromBasketCommandHandler(IBasketWriteRepository basketWriteRepository)
        {
            _basketWriteRepository = basketWriteRepository;
        }

        public async  Task<RemoveItemFromBasketCommandResponse> Handle(RemoveItemFromBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var result =  await _basketWriteRepository.RemoveItemFromBasketAsync(request.UserId, request.bookId);

            if (result ==false)
            {
                return new()
                {
                    Success = false,
                    Message = "Failed to remove the product from the basket. Please try again."

                };


            }


            return new()
            {

                Success = true,
                Message = "Book successfully removed from the basket."
            };






        }
    }
}
