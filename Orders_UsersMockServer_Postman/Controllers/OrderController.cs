using Microsoft.AspNetCore.Mvc;
using Orders_UsersMockServer_Postman.Models;
using Orders_UsersMockServer_Postman.Services;

namespace Orders_UsersMockServer_Postman.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //    public class OrderController : ControllerBase
    //    {
    //        private readonly IOrderService _orderService;

    //        public OrderController(IOrderService orderService )
    //        {
    //            _orderService = orderService;
    //        }

    //        [HttpPost("create")]
    //        public async Task<IActionResult> CreateOrder(int id)
    //        {
    //            var order = await _orderService.CreateOrderAsync(id);
    //            if (order == null)
    //            {
    //                return BadRequest("Failed to create order.");
    //            }

    //            return Ok(order);
    //        }
    //    }
    //}

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order orderDto)
        {
            if (orderDto == null || orderDto.User==null || orderDto.User.userId <= 0 || string.IsNullOrEmpty(orderDto.ProductName))
            {
                return BadRequest("Invalid order data.");
            }

            var order = await _orderService.CreateOrderAsync(orderDto);

            if (order == null)
            {
                return NotFound("User not found.");
            }

            return Ok(order);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        // New method to get order by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(order);
        }


        // New method to delete order by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var isDeleted = await _orderService.DeleteOrderAsync(id);

            if (!isDeleted)
            {
                return NotFound("Order not found.");
            }

            return NoContent(); // Return a 204 No Content response if deleted
        }

        // Implement PUT method to update order by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrderDto)
        {
            if (updatedOrderDto == null || id <= 0)
            {
                return BadRequest("Invalid order data.");
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(id, updatedOrderDto);

            if (updatedOrder == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(updatedOrder);
        }
    }
}
