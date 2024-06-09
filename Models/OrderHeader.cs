using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService.Models
{
    public class OrderHeader
    {
        public int orderHeaderId { get; set; }
        public string? userName { get; set; }
        public DateTime orderDate { get; set; }
    }
}