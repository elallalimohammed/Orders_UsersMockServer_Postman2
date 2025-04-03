using Moq;
using Orders_UsersMockServer_Postman.Controllers;
using Orders_UsersMockServer_Postman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Protected;
using System.Text.Json;
using System.Net.Http;
using Orders_UsersMockServer_Postman.Services;
using Orders_UsersMockServer_Postman.Repositories;
using Orders_UsersMockServer_Postman.Models;

namespace Orders_UsersMockService_TestProject
{

    [TestClass]
    public class OrderServiceTests
    {

        private HttpClient _httpClient = new HttpClient();
        private Mock<IOrderRepository> _mockOrderRepository;
        private OrderService _orderService;
        

        [TestInitialize]
        public void Setup()
        {
        // Mock the repository
         _mockOrderRepository = new Mock<IOrderRepository>();

        // Create instance of OrderService using mocked repository
        _orderService = new OrderService(_httpClient, _mockOrderRepository.Object); // Passing null for HttpClient, since we're mocking the HTTP call
        }

        [TestMethod]
        public async Task CreateOrderAsync_ShouldFetchUserAndSaveOrder_WithMockedRepository()
        {
            // Arrange
            var testOrder = new Order
            {
                Id = 0, // Initially no ID
                ProductName = "Laptop",
                Quantity = 2,
                User = new User { userId = 1 } // Setting userId, user will be mocked in the service
            };

            // Mock repository save method
            _mockOrderRepository
                .Setup(repo => repo.AddOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order o) =>
                {
                    o.Id = 1; // Simulate assignment of an ID when the order is saved
                    return o;
                });

            // Act: We're not mocking the HTTP call here, just passing a mocked user into the service.
            var result = await _orderService.CreateOrderAsync(testOrder);

            // Assert
            Assert.IsNotNull(result);  // Ensure the order is not null
            Assert.AreEqual(1, result.Id);  // Ensure the ID is assigned correctly (from the mocked repo)
            Assert.IsNotNull(result.User);  // Ensure the user is not null
            Assert.AreEqual(1, result.User.userId);  // Ensure the userId is correct
            Assert.AreEqual("Mohammed", result.User.Name);  // Ensure the correct user name is assigned
            Assert.AreEqual("moal@easj.dk", result.User.Email);  // Ensure the correct user email is assigned

            // Verify that AddOrderAsync was called once
            _mockOrderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<Order>()), Times.Once);
        }
    }

}


