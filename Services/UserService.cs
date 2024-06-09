using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ShippingService.DTO;
using ShippingService.Models;

namespace ShippingService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5286");
        }

        public async Task<IEnumerable<Users>> GetAllUser()
        {
            var response = await _httpClient.GetAsync("/users");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<IEnumerable<Users>>(results);
                if (user == null)
                {
                    throw new ArgumentException("Cannot get users");
                }
                return user;
            }
            else
            {
                throw new ArgumentException($"Cannot get User - httpstatus: {response.StatusCode}");
            }
        }

        public async Task<Users> GetUserByName(string username)
        {
            var response = await _httpClient.GetAsync($"/users/{username}");
            if (response.IsSuccessStatusCode)
            {
                var results = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<Users>(results);
                if (user == null)
                {
                    throw new ArgumentException("Cannot get Users");
                }
                return user;
            }
            else
            {
                throw new ArgumentException($"Cannot get Users - httpstatus: {response.StatusCode}");
            }
        }

        public async Task UpdateUserBalance(UserUpdateBalance userUpdateBalance)
        {
            var json = JsonSerializer.Serialize(userUpdateBalance);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/users/updateShippingBalance", data);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Cannot update User Balance - httpstatus: {response.StatusCode}");
            }
        }
        public async Task UpdateUserBackBalance(UserUpdateBalance userUpdateBalance)
        {
            var json = JsonSerializer.Serialize(userUpdateBalance);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/users/updateBackBalance", data);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Cannot Cancle update User Balance - httpstatus: {response.StatusCode}");
            }
        }
    }
}