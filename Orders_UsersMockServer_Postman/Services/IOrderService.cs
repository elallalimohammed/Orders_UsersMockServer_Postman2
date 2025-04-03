using Orders_UsersMockServer_Postman.Models;

namespace Orders_UsersMockServer_Postman.Services
{

    public interface IOrderService
    {

        Task<Order> CreateOrderAsync(Order newOrder);
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<Order> UpdateOrderAsync(int id, Order updatedOrderDto);
    }
}
