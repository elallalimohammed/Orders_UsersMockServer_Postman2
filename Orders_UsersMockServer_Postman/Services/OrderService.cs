using System.Text.Json;
using Orders_UsersMockServer_Postman.Models;
using Orders_UsersMockServer_Postman.Repositories;

namespace Orders_UsersMockServer_Postman.Services
{
    //comment
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private static int _orderCounter = 1; // Simulating order ID generation
        private readonly IOrderRepository _orderRepository;
        public OrderService(HttpClient httpClient, IOrderRepository orderRepository)
        {
            _httpClient = httpClient;
            _orderRepository = orderRepository;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            var userResponse = await _httpClient.GetStringAsync($"https://dc56817d-0fe6-43d8-b919-24aefc7e4568.mock.pstmn.io/Users?userid={order.User.userId}");

            if (string.IsNullOrEmpty(userResponse))
                return null;

            var user = JsonSerializer.Deserialize<User>(userResponse);

            order.User = user;

            order.CreatedAt = DateTime.Now;

            return await _orderRepository.AddOrderAsync(order);
        }
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        // New method to fetch order by ID
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        // Add method to delete order by ID
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }

        // Implement update order by ID
        public async Task<Order> UpdateOrderAsync(int id, Order updatedOrderDto)
        {
            var updatedOrder = new Order
            {
                ProductName = updatedOrderDto.ProductName,
                Quantity = updatedOrderDto.Quantity,
                // CreatedAt = DateTime.UtcNow // You can update the creation date or keep it unchanged
            };

            return await _orderRepository.UpdateOrderAsync(id, updatedOrder);
        }
    }
}
