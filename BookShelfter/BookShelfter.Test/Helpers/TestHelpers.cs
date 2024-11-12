using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Commands.Basket.AddItem;
using BookShelfter.Domain.Entities;

namespace BookShelfter.Test.Helpers
{
    public class TestHelpers
    {
        public static Book CreateTestBook(int id = 11, string bookName = "Test Book", decimal price = 10)
        {
            return new Book
            {
                Id = id,
                BookName = bookName,
                Price = price,
                AuthorName = "Test Author",
                Stock = 10,
                Description = "Test Description",
                CategoryId = 1


            };

        }


        public static Basket CreateTestBasket(string userId = "test-user-id")
        {
            return new Basket
            {
                UserId = userId,
                BasketItems = new List<BasketItem>
                {
                    new BasketItem()
                    {
                        BookId = 11,
                        Quantity = 1,
                        UnitPrice = 10,
                        Book = new Book
                        {
                            Id = 11,
                            BookName = "Test Book",
                            AuthorName = "Test Author",
                            Stock = 10,
                            Price = 10,
                            Description = "Test Description",
                            CategoryId = 1
                        }
                    }
                }
            };
        }



        public static AddItemToBasketCommandRequest CreateTestAddItemToBasketCommandRequest(string userId = "test-user-id", int bookId = 11, int quantity = 1)
        {
            return new AddItemToBasketCommandRequest
            {
                UserID = userId,
                bookId = bookId,
                Quantity = quantity
            };
        }
    }
}
