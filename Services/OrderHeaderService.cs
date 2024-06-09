using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Services
{
    public class OrderHeaderService : IOrderHeaderService
    {

        private readonly HttpClient _httpClient;

        public OrderHeaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5164");
        }

        public async Task<IEnumerable<OrderHeader>> GetAll()
        {
            var response = await _httpClient.GetAsync("/orderHeaders");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<IEnumerable<OrderHeader>>(results);
                if (order == null)
                {
                    throw new ArgumentException("Cannot get Order Header");
                }
                return order;
            }
            else
            {
                throw new ArgumentException($"Cannot get Order Header - httpstatus: {response.StatusCode}");
            }
        }

        public Task<IEnumerable<OrderHeader>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderHeader> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/orderHeaders/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<OrderHeader>(results);
                if (order == null)
                {
                    throw new ArgumentException("Cannot get Users");
                }
                return order    ;
            }
            else
            {
                throw new ArgumentException($"Cannot get Users - httpstatus: {response.StatusCode}");
            }
        }
    }
}