using System.Runtime.InteropServices.JavaScript;
using BookShelfter.Domain.Entities.Common;
using BookShelfter.Domain.Entities.Enums;

namespace BookShelfter.Domain.Entities;

public class Order:BaseEntity
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; } 
    public string?  Description { get; set; }
    public string DeliveryAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string PaymentMethod { get; set; }

    public string OrderCode { get; set; }
    public int ? BasketId { get; set; }
    public Basket Basket { get; set; }
    public CompletedOrder CompletedOrder { get; set; }


    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
            
    public ICollection<OrderDetails> OrderDetails { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}