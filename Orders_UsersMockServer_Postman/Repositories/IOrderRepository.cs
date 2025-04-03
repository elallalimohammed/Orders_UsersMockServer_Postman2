using Orders_UsersMockServer_Postman.Models;

namespace Orders_UsersMockServer_Postman.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id); // Fetch order by ID
        Task<bool> DeleteOrderAsync(int id); // Delete order by ID
        Task<Order> UpdateOrderAsync(int id, Order updatedOrder); // Update order by ID


    }
}
