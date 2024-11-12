using BookShelfter.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Basket;

namespace BookShelfter.Application.Features.Commands.Basket.AddItem
{
    public class AddItemToBasketCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; } 
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public BasketDto UpdatedBasket { get; set; } 
    }
}
