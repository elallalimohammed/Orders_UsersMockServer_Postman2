using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Orders_UsersMockServer_Postman;
using Orders_UsersMockServer_Postman.Controllers;
using Orders_UsersMockServer_Postman.Services;
using Orders_UsersMockServer_Postman.Models;

namespace Orders_UsersMockService_TestProject
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private OrderController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Initialize the mock and the controller before each test
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }

        [TestMethod]
        public async Task CreateOrder_ReturnsOkResult_WhenOrderIsCreated()
        {
            // Arrange
            var orderDto = new Order
            {
                User = new User() { Name = "Mohammed", Email = "moal@eaaa.dk", userId = 1 },
                ProductName = "Laptop",
                Quantity = 1
            };

            var createdOrder = new Order
            {
                Id = 1,
                User = new User() { Name = "Mohammed", Email = "moal@eaaa.dk", userId = 1 },
                ProductName = "Laptop",
                Quantity = 1
                //,
                //CreatedAt = DateTime.UtcNow
            };

            // Mock the service method to return a created order
            _mockOrderService.Setup(service => service.CreateOrderAsync(orderDto)).ReturnsAsync(createdOrder);

            // Act
            var result = await _controller.CreateOrder(orderDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var returnValue = ((OkObjectResult)result).Value as Order;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1, returnValue.Id);
            Assert.AreEqual("Mohammed", returnValue.User.Name);
            Assert.AreEqual("Laptop", returnValue.ProductName);
        }

        [TestMethod]
        public async Task CreateOrder_With_User_Null()
        {
            // Arrange
            var orderDto = new Order
            {
                User = null,
                ProductName = "Laptop",
                Quantity = 1
            };

            var createdOrder = new Order
            {
                Id = 1,
                // User = new User() { Name = "Mohammed", Email = "moal@eaaa.dk", userId = 1 },
                ProductName = "Laptop",
                Quantity = 1
                ,
                CreatedAt = DateTime.UtcNow
            };

            // Mock the service method to return a created order
            _mockOrderService.Setup(service => service.CreateOrderAsync(orderDto)).ReturnsAsync(createdOrder);

            // Act
            var result = await _controller.CreateOrder(orderDto);

            // Assert
            //Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            //var returnValue = ((OkObjectResult)result).Value as Order;
            //Assert.IsNotNull(returnValue);
            //Assert.AreEqual(1, returnValue.Id);
            //Assert.AreEqual("Laptop", returnValue.ProductName);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task CreateOrder_ReturnsBadRequest_WhenOrderDtoIsInvalid()
        {
            // Arrange
            Order orderDto = null; // Invalid order

            // Act
            var result = await _controller.CreateOrder(orderDto);

            // Assert
           Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}