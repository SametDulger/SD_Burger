using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;

namespace SD_Burger.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IApiService _apiService;

        public OrdersController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<OrderViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _apiService.GetAsync<OrderViewModel>($"orders/{id}");
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sipariş detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var waiters = await _apiService.GetAsync<List<UserViewModel>>("users");
                var orderStatuses = await _apiService.GetAsync<List<object>>("dropdown/order-statuses");
                var orderPriorities = await _apiService.GetAsync<List<object>>("dropdown/order-priorities");

                ViewBag.Tables = tables?.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Masa {t.TableNumber} - {t.Capacity} Kişilik"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Waiters = waiters?.Select(w => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.FirstName} {w.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderStatuses = orderStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderPriorities = orderPriorities?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Tables = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Waiters = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderPriorities = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel createOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _apiService.PostAsync<OrderViewModel>("orders", createOrderViewModel);
                    TempData["Success"] = "Sipariş başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = order.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sipariş oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var waiters = await _apiService.GetAsync<List<UserViewModel>>("users");
                var orderStatuses = await _apiService.GetAsync<List<object>>("dropdown/order-statuses");
                var orderPriorities = await _apiService.GetAsync<List<object>>("dropdown/order-priorities");

                ViewBag.Tables = tables?.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Masa {t.TableNumber} - {t.Capacity} Kişilik"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Waiters = waiters?.Select(w => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.FirstName} {w.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderStatuses = orderStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderPriorities = orderPriorities?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Tables = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Waiters = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderPriorities = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createOrderViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var order = await _apiService.GetAsync<OrderViewModel>($"orders/{id}");
                if (order == null)
                {
                    return NotFound();
                }

                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var waiters = await _apiService.GetAsync<List<UserViewModel>>("users");
                var orderStatuses = await _apiService.GetAsync<List<object>>("dropdown/order-statuses");
                var orderPriorities = await _apiService.GetAsync<List<object>>("dropdown/order-priorities");

                ViewBag.Tables = tables?.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Masa {t.TableNumber} - {t.Capacity} Kişilik"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Waiters = waiters?.Select(w => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.FirstName} {w.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderStatuses = orderStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderPriorities = orderPriorities?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

                var updateOrderViewModel = new UpdateOrderViewModel
                {
                    CustomerName = order.CustomerName,
                    CustomerPhone = order.CustomerPhone,
                    Notes = order.Notes,
                    Status = order.Status,
                    Priority = order.Priority,
                    TableId = order.TableId,
                    BranchId = order.BranchId,
                    WaiterId = order.WaiterId
                };

                return View(updateOrderViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sipariş bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOrderViewModel updateOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _apiService.PutAsync<OrderViewModel>($"orders/{id}", updateOrderViewModel);
                    TempData["Success"] = "Sipariş başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = order.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sipariş güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var waiters = await _apiService.GetAsync<List<UserViewModel>>("users");
                var orderStatuses = await _apiService.GetAsync<List<object>>("dropdown/order-statuses");
                var orderPriorities = await _apiService.GetAsync<List<object>>("dropdown/order-priorities");

                ViewBag.Tables = tables?.Select(t => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Masa {t.TableNumber} - {t.Capacity} Kişilik"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Waiters = waiters?.Select(w => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.FirstName} {w.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderStatuses = orderStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.OrderPriorities = orderPriorities?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Tables = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Waiters = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.OrderPriorities = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updateOrderViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _apiService.GetAsync<OrderViewModel>($"orders/{id}");
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sipariş bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"orders/{id}");
                TempData["Success"] = "Sipariş başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sipariş silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByBranch(int branchId)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/branch/{branchId}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByTable(int tableId)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/table/{tableId}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByStatus(string status)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/status/{status}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByPriority(string priority)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/priority/{priority}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByWaiter(int waiterId)
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>($"orders/waiter/{waiterId}");
                return View("Index", orders);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Siparişler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GenerateOrderNumber()
        {
            try
            {
                var orderNumber = await _apiService.GetAsync<string>("orders/generate-number");
                return Json(new { orderNumber });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
} 