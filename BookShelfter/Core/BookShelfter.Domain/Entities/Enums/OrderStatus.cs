using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Domain.Entities.Enums
{
    public  enum  OrderStatus
    {
        Pending,    
        Processing,  
        Shipped,    
        Delivered,  
        Cancelled    
    }
}
