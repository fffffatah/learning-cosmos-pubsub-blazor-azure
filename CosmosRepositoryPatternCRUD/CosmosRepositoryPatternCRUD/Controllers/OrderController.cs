﻿using CosmosRepositoryPatternCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using Polly;
using Polly.Retry;

namespace CosmosRepositoryPatternCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ILogger<OrderController> _logger;
        IRepository<Order> _orderRepository;
        AsyncRetryPolicy<ActionResult> _retryPolicy;
        public OrderController(ILogger<OrderController> logger, IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _retryPolicy = Policy<ActionResult>.Handle<HttpRequestException>().RetryAsync(retryCount: 5);
        }
        [Route("create/order")]
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromForm] Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            order.Type = "Order";
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _orderRepository.CreateAsync(order);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = "Order Added" });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to add order!" });
            });
        }
    }
}
