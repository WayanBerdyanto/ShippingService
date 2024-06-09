using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Services
{
    public interface IOrderHeaderService
    {
        Task<IEnumerable<OrderHeader>> GetAll();
        Task<OrderHeader> GetUserById(int id);
    }
}