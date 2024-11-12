using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.DTOs.Basket
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
