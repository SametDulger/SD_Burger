using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;
using SD_Burger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            try
            {
                var orders = await _orderService.GetAllAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                    return NotFound(new { message = "Sipariş bulunamadı." });

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto createOrderDto)
        {
            try
            {
                var order = await _orderService.CreateAsync(createOrderDto);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> Update(int id, UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _orderService.UpdateAsync(id, updateOrderDto);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("branch/{branchId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByBranch(int branchId)
        {
            try
            {
                var orders = await _orderService.GetByBranchAsync(branchId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("table/{tableId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByTable(int tableId)
        {
            try
            {
                var orders = await _orderService.GetByTableAsync(tableId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByStatus(OrderStatus status)
        {
            try
            {
                var orders = await _orderService.GetByStatusAsync(status);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("waiter/{waiterId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByWaiter(int waiterId)
        {
            try
            {
                var orders = await _orderService.GetByWaiterAsync(waiterId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("generate-order-number")]
        public async Task<ActionResult<string>> GenerateOrderNumber()
        {
            try
            {
                var orderNumber = await _orderService.GenerateOrderNumberAsync();
                return Ok(new { orderNumber });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş numarası oluşturulurken hata oluştu.", error = ex.Message });
            }
        }
    }
} 