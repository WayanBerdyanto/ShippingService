using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService.DTO
{
    public class UpdateOrderHeader
    {
        public int ShippingId { get; set; }
        public int OrderHeaderId { get; set; }
        public int CurrierId { get; set;}
        public string ShippingAddress { get; set; }
        public string ShippingVendor { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? ShippingStatus { get; set; }         
        public string ShippingInformation { get; set; }
        public DateTime EstimatedShipping { get; set; }
        public decimal ItemWeight { get; set; }
        public decimal ShippingCosts { get; set; }
    }
}