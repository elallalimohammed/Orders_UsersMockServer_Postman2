using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MongoDB.Driver;
using System.Threading.Tasks;
using Orders_UsersMockServer_Postman.Repositories;
using Orders_UsersMockServer_Postman.Models;

[TestClass]
public class OrderRepositoryTests
{
    // creating mocks for collection, database og MongoClient
    private Mock<IMongoDatabase> _mockDatabase;
    private Mock<IMongoCollection<Order>> _mockCollection;
    private OrderRepository _orderRepository;

    [TestInitialize]
    public void Setup()
    {      
        _mockCollection = new Mock<IMongoCollection<Order>>();

        _mockDatabase = new Mock<IMongoDatabase>();

        // 2️⃣ Set up `GetCollection<Order>` to return our mock collection
        _mockDatabase.Setup(db => db.GetCollection<Order>("orders", null))
            .Returns(_mockCollection.Object);

        // 3️⃣ Create repository using the mocked database
        _orderRepository = new OrderRepository(_mockDatabase.Object);
    }

    [TestMethod]
    public async Task SaveOrderAsync_ShouldInsertOrderIntoDatabase()
    {
        // Arrange
        var testOrder = new Order { ProductName = "Laptop", Quantity = 2 };

        // Act
        await _orderRepository.AddOrderAsync(testOrder);

        // Assert
        _mockCollection.Verify(coll => coll.InsertOneAsync(It.IsAny<Order>(), null, default), Times.Once);
    }
}
