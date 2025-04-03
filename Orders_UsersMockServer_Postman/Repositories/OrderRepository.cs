using MongoDB.Driver;
using Orders_UsersMockServer_Postman.Models;

namespace Orders_UsersMockServer_Postman.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        //private readonly List<Order> _orders = new();
        private static int _orderCounter = 1; // Simulating order ID auto-increment

        private readonly IMongoCollection<Order> _ordersCollection;

        public OrderRepository(IMongoDatabase database) // Inject database
        {
            if (database == null) throw new ArgumentNullException(nameof(database));

            _ordersCollection = database.GetCollection<Order>("orders");
        }

        //public OrderRepository()
        //{
        //    var client = new MongoClient("mongodb://localhost:27017"); // Change if needed
        //    var database = client.GetDatabase("OrdersDB_MockUsers");
        //    _ordersCollection = database.GetCollection<Order>("orders");
        //}
        //public async Task SaveOrderAsync(Order order)
        //{
        //    await _ordersCollection.InsertOneAsync(order);
        //}

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _ordersCollection.Find(order => true).ToListAsync();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            Random random = new Random();
            order.Id= random.Next(7, 101); // Assign a unique ID
            await _ordersCollection.InsertOneAsync(order);
            return await Task.FromResult(order);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _ordersCollection.Find(order => order.Id == id).FirstOrDefaultAsync();
        }

        // Implement delete order by ID
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var result = await _ordersCollection.DeleteOneAsync(order => order.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<Order> UpdateOrderAsync(int id, Order updatedOrder)
        {
            // Step 1: Find the order by ID
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);

            // Step 2: Define the update (which fields to change)
            var update = Builders<Order>.Update
                .Set(o => o.ProductName, updatedOrder.ProductName)
                .Set(o => o.Quantity, updatedOrder.Quantity);

            // Step 3: Set options to return the updated document
            var options = new FindOneAndUpdateOptions<Order>
            {
                ReturnDocument = ReturnDocument.After // Get updated order after update
            };

            // Step 4: Apply update and return the updated order
            var updateredOrder = await _ordersCollection.FindOneAndUpdateAsync(filter, update, options);

            return updateredOrder;
        }















        //public async Task<bool> DeleteOrderAsync(int id)
        //{
        //    var order = _orders.FirstOrDefault(o => o.Id == id);
        //    if (order != null)
        //    {
        //        _orders.Remove(order);
        //        return await Task.FromResult(true);
        //    }
        //    return await Task.FromResult(false);
        //    return false;
        //}

        //// Implement update order by ID
        //public async Task<Order> UpdateOrderAsync(int id, Order updatedOrder)
        //{
        //    var existingOrder = _orders.FirstOrDefault(o => o.Id == id);

        //    if (existingOrder != null)
        //    {
        //        existingOrder.ProductName = updatedOrder.ProductName;
        //        existingOrder.Quantity = updatedOrder.Quantity;
        //        //existingOrder.CreatedAt = updatedOrder.CreatedAt; // Assuming you want to update the creation date too
        //        return await Task.FromResult(existingOrder);
        //    }

        //    return null;
        //}

    }
}
