using E_Commerce.Core.DTOs;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var result = await _orderService.GetAllOrder();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.GetOrderByID(id);
                if (result == null)
                    return NotFound(new { code = -1, message = "Order not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
        [HttpPost("init")]
        public async Task<IActionResult> addorders([FromBody] OrderDTO orderdto)
        {
            try
            {
                int order_id = await _orderService.AddOrder(orderdto);
                return Ok(order_id);
            }
            catch (Exception ex)
            {
                var errorresponse = new { code = -5, message = ex.Message };
                return BadRequest(errorresponse);
            }
        }
        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.ConfirmOrder(id);
                if (result == null)
                    return NotFound(new { code = -1, message = "Order not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
    }

}
