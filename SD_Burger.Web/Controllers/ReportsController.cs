using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Text.Json;

namespace SD_Burger.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IApiService _apiService;

        public ReportsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var dashboardData = new DashboardReportViewModel
                {
                    TotalOrders = await GetTotalOrders(),
                    TotalRevenue = await GetTotalRevenue(),
                    TotalUsers = await GetTotalUsers(),
                    TotalBranches = await GetTotalBranches(),
                    RecentOrders = await GetRecentOrders(),
                    TopMenuItems = await GetTopSellingItems(),
                    MonthlyRevenue = GetMonthlyRevenue()
                };

                return View(dashboardData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Raporlar yüklenirken hata oluştu: " + ex.Message;
                return View(new DashboardReportViewModel());
            }
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> SalesReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var salesData = new SalesReportViewModel
                {
                    StartDate = startDate ?? DateTime.Today.AddDays(-30),
                    EndDate = endDate ?? DateTime.Today,
                    SalesData = await GetSalesData(startDate ?? DateTime.Today.AddDays(-30), endDate ?? DateTime.Today)
                };

                return View(salesData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Satış raporu yüklenirken hata oluştu: " + ex.Message;
                return View(new SalesReportViewModel());
            }
        }

        public async Task<IActionResult> InventoryReport()
        {
            try
            {
                var inventoryData = new InventoryReportViewModel
                {
                    LowStockInventory = await GetLowStockItems(),
                    OutOfStockInventory = await GetOutOfStockItems(),
                    InventoryValue = await GetInventoryValue()
                };

                return View(inventoryData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Envanter raporu yüklenirken hata oluştu: " + ex.Message;
                return View(new InventoryReportViewModel());
            }
        }

        public async Task<IActionResult> UserReport()
        {
            try
            {
                var userData = new UserReportViewModel
                {
                    TotalUsers = await GetTotalUsers(),
                    ActiveUsers = await GetActiveUsers(),
                    UserRoles = await GetUserRoles(),
                    RecentRegistrations = await GetRecentRegistrations()
                };

                return View(userData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı raporu yüklenirken hata oluştu: " + ex.Message;
                return View(new UserReportViewModel());
            }
        }

        private async Task<int> GetTotalOrders()
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                return orders?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<decimal> GetTotalRevenue()
        {
            try
            {
                var payments = await _apiService.GetAsync<List<PaymentViewModel>>("payments");
                return payments?.Where(p => p.Status == "Completed").Sum(p => p.Amount) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<int> GetTotalUsers()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                return users?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<int> GetTotalBranches()
        {
            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                return branches?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<List<OrderViewModel>> GetRecentOrders()
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                return orders?.Take(5).ToList() ?? new List<OrderViewModel>();
            }
            catch
            {
                return new List<OrderViewModel>();
            }
        }

        private async Task<List<TopSellingItemViewModel>> GetTopSellingItems()
        {
            try
            {
                var menuItems = await _apiService.GetAsync<List<MenuItemViewModel>>("menuitems");
                return menuItems?.Take(5).Select(m => new TopSellingItemViewModel
                {
                    Name = m.Name,
                    Quantity = 0, // Bu değer gerçek uygulamada sipariş verilerinden hesaplanır
                    Revenue = m.Price,
                    TotalRevenue = m.Price,
                    TotalSales = m.Price
                }).ToList() ?? new List<TopSellingItemViewModel>();
            }
            catch
            {
                return new List<TopSellingItemViewModel>();
            }
        }

        private List<MonthlyRevenueData> GetMonthlyRevenue()
        {
            // Bu metod gerçek uygulamada veritabanından aylık gelir verilerini çeker
            var monthlyData = new List<MonthlyRevenueData>();
            for (int i = 11; i >= 0; i--)
            {
                monthlyData.Add(new MonthlyRevenueData
                {
                    Month = DateTime.Today.AddMonths(-i).ToString("MMM yyyy"),
                    Revenue = new Random().Next(10000, 50000)
                });
            }
            return monthlyData;
        }

        private async Task<List<SalesData>> GetSalesData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                
                var salesData = orders?
                    .Where(o => o.OrderDate.HasValue && o.OrderDate.Value >= startDate && o.OrderDate.Value <= endDate)
                    .GroupBy(o => o.OrderDate.Value.Date)
                    .Select(g => new SalesData
                    {
                        Date = g.Key.ToString("dd/MM/yyyy"),
                        Orders = g.Count(),
                        Revenue = g.Sum(o => o.TotalAmount)
                    })
                    .ToList() ?? new List<SalesData>();

                return salesData;
            }
            catch
            {
                return new List<SalesData>();
            }
        }

        private async Task<List<InventoryViewModel>> GetLowStockItems()
        {
            try
            {
                var inventory = await _apiService.GetAsync<List<InventoryViewModel>>("inventory");
                return inventory?.Where(i => i.CurrentStock < 50).ToList() ?? new List<InventoryViewModel>();
            }
            catch
            {
                return new List<InventoryViewModel>();
            }
        }

        private async Task<List<InventoryViewModel>> GetOutOfStockItems()
        {
            try
            {
                var inventory = await _apiService.GetAsync<List<InventoryViewModel>>("inventory");
                return inventory?.Where(i => i.CurrentStock == 0).ToList() ?? new List<InventoryViewModel>();
            }
            catch
            {
                return new List<InventoryViewModel>();
            }
        }

        private async Task<decimal> GetInventoryValue()
        {
            try
            {
                var inventory = await _apiService.GetAsync<List<InventoryViewModel>>("inventory");
                return inventory?.Sum(i => i.CurrentStock * 10) ?? 0; // Örnek fiyat
            }
            catch
            {
                return 0;
            }
        }

        private async Task<int> GetActiveUsers()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                return users?.Count(u => u.IsActive) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<List<UserRoleData>> GetUserRoles()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                
                var roleData = users?
                    .GroupBy(u => u.Role)
                    .Select(g => new UserRoleData
                    {
                        Role = g.Key,
                        Count = g.Count()
                    })
                    .ToList() ?? new List<UserRoleData>();

                return roleData;
            }
            catch
            {
                return new List<UserRoleData>();
            }
        }

        private async Task<List<UserViewModel>> GetRecentRegistrations()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                return users?.Take(5).ToList() ?? new List<UserViewModel>();
            }
            catch
            {
                return new List<UserViewModel>();
            }
        }
    }
} 