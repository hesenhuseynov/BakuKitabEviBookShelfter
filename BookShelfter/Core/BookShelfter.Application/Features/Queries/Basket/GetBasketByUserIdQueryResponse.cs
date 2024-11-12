using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Basket;
using BookShelfter.Domain.Entities;

namespace BookShelfter.Application.Features.Queries.Basket
{
    public class GetBasketByUserIdQueryResponse
    {
        public  ICollection<BasketItemDto> BasketItems { get; set;  }
        public  bool Success  { get; set; }

        public  string Message { get; set; }
    }
}
