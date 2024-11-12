using BookShelfter.Application.DTOs.Basket;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;
using Serilog;

namespace BookShelfter.Application.Features.Commands.Basket.AddItem
{
    public  class BasketHelper
    {
        private static  ILogger _logger;

        public static void ConfigureLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static BasketItemDto MapToBasketItemDto(BasketItem basketItem)
        {

            if (basketItem.Book == null)
            {
                _logger?.Error("BasketItem.Book is null. BasketItem ID: {BasketItemId}, Basket ID: {BasketId}",
                    basketItem.Id,
                    basketItem.BasketId);
                throw new ArgumentNullException(nameof(basketItem.Book), "Book cannot be null");
            }
            Log.Information("This is a test log message.");

            return new BasketItemDto
            {
                BasketId = basketItem.BasketId,
                BookId = basketItem.BookId,
                Quantity = basketItem.Quantity,
                UnitPrice = basketItem.UnitPrice,
                Book = new BookDto
                {
                    BookName = basketItem.Book.BookName,
                    AuthorName = basketItem.Book.AuthorName,
                    Stock = basketItem.Book.Stock,
                    Price = basketItem.Book.Price,
                    Description = basketItem.Book.Description,
                    CategoryId = basketItem.Book.CategoryId,
                    BookId = basketItem.Book.Id
                },
                Id = basketItem.Id,
                CreatedDate = basketItem.CreatedDate,
                UpdatedDate = basketItem.UpdatedDate
            };
        }


        public static BasketDto MapToBasketDto(Domain.Entities.Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket), "Basket cannot be null");
            }

            return new BasketDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                BasketItems = basket.BasketItems.Select(MapToBasketItemDto).ToList(),
                CreatedDate = basket.CreatedDate,
                UpdatedDate = basket.UpdatedDate
            };
        }
    }
}
