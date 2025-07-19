using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly IUserService _userService;

        public ReportsController(
            IOrderService orderService,
            IInventoryService inventoryService,
            IUserService userService)
        {
            _orderService = orderService;
            _inventoryService = inventoryService;
            _userService = userService;
        }

        [HttpGet("sales")]
        public async Task<ActionResult<SalesReportDto>> GetSalesReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var salesReport = await _orderService.GetSalesReportAsync(startDate, endDate);
                return Ok(salesReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("inventory")]
        public async Task<ActionResult<InventoryReportDto>> GetInventoryReport()
        {
            try
            {
                var inventoryReport = await _inventoryService.GetInventoryReportAsync();
                return Ok(inventoryReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<UserReportDto>> GetUserReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var userReport = await _userService.GetUserReportAsync(startDate, endDate);
                return Ok(userReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardReportDto>> GetDashboardReport()
        {
            try
            {
                var dashboardReport = new DashboardReportDto
                {
                    TotalOrders = await _orderService.GetTotalOrdersAsync(),
                    TotalSales = await _orderService.GetTotalSalesAsync(),
                    ActiveUsers = await _userService.GetActiveUsersCountAsync(),
                    LowStockItems = await _inventoryService.GetLowStockItemsCountAsync()
                };
                return Ok(dashboardReport);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 