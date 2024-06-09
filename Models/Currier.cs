using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService.Models
{
    public class Currier
    {
        public int CurrierId { get; set; }
        public string CurrierName { get; set; }
        public string CurrierAddress { get; set; }
        public string CurrierPhone { get; set; }
    }
}