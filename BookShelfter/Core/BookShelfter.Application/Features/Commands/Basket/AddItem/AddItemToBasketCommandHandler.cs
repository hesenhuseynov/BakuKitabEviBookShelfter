using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Basket;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.DTOs.User;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Basket.AddItem
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBookReadRepository _bookReadRepository;
        public AddItemToBasketCommandHandler(IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBookReadRepository bookReadRepository)
        {
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _bookReadRepository = bookReadRepository;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookReadRepository.GetByIdAsync(request.bookId);
            if (book == null)
            {
                return new AddItemToBasketCommandResponse
                {
                    Message = "Invalid Book ID",
                    Success = false
                };
            }


            var basket = await _basketReadRepository.GetByUserIdAsync(request.UserID);


            if (basket == null)
            {
                basket = new Domain.Entities.Basket() 
                    { UserId = request.UserID, BasketItems = new List<BasketItem>() };

                await _basketWriteRepository.AddAsync(basket);
                await _basketWriteRepository.SaveAsync();

            }

            //if (basket == null)
            //{
            //    throw new InvalidOperationException("Basket cannot be null.");
            //}


            if (basket.BasketItems == null)
            {
                basket.BasketItems=new List<BasketItem>();
            }

            var existingItem = basket.BasketItems.FirstOrDefault(i => i.BookId == request.bookId);

            if (existingItem !=null)
            {
                existingItem.Quantity += request.Quantity;
            }

            else
            {
                var basketItem = new BasketItem
                {
                    BookId = request.bookId,
                    Quantity = request.Quantity,
                    UnitPrice = book.Price,
                    Book = book
                };

                basket.BasketItems.Add(basketItem);

            }


            _basketWriteRepository.Update(basket);
            await _basketWriteRepository.SaveAsync();


            return new AddItemToBasketCommandResponse
            {
                Success = true,
                Message = "Book added to basket successfully.",
                UpdatedBasket = BasketHelper.MapToBasketDto(basket) 
            };

            



        }
    }
}
