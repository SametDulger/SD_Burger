using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Text.Json;

namespace SD_Burger.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IApiService _apiService;

        public PaymentsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var payments = await _apiService.GetAsync<List<PaymentViewModel>>("payments");
                return View(payments);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ödemeler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<PaymentViewModel>());
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                var paymentMethods = await _apiService.GetAsync<List<object>>("dropdown/payment-methods");
                var paymentStatuses = await _apiService.GetAsync<List<object>>("dropdown/payment-statuses");
                
                ViewBag.Orders = orders?.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Sipariş #{o.Id} - {o.CustomerName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentMethods = paymentMethods?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentStatuses = paymentStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sipariş listesi yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Orders = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentMethods = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePaymentViewModel createPaymentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = await _apiService.PostAsync<PaymentViewModel>("payments", createPaymentViewModel);
                    TempData["Success"] = "Ödeme başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = payment.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ödeme oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                var paymentMethods = await _apiService.GetAsync<List<object>>("dropdown/payment-methods");
                var paymentStatuses = await _apiService.GetAsync<List<object>>("dropdown/payment-statuses");
                
                ViewBag.Orders = orders?.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Sipariş #{o.Id} - {o.CustomerName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentMethods = paymentMethods?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentStatuses = paymentStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Orders = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentMethods = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createPaymentViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var payment = await _apiService.GetAsync<PaymentViewModel>($"payments/{id}");
                if (payment == null)
                {
                    return NotFound();
                }
                return View(payment);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ödeme detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var payment = await _apiService.GetAsync<PaymentViewModel>($"payments/{id}");
                if (payment == null)
                {
                    return NotFound();
                }

                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                var paymentMethods = await _apiService.GetAsync<List<object>>("dropdown/payment-methods");
                var paymentStatuses = await _apiService.GetAsync<List<object>>("dropdown/payment-statuses");
                
                ViewBag.Orders = orders?.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Sipariş #{o.Id} - {o.CustomerName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentMethods = paymentMethods?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentStatuses = paymentStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

                var updatePaymentViewModel = new UpdatePaymentViewModel
                {
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod,
                    Status = payment.Status,
                    TransactionId = payment.TransactionId,
                    Notes = payment.Notes
                };

                return View(updatePaymentViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ödeme bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePaymentViewModel updatePaymentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = await _apiService.PutAsync<PaymentViewModel>($"payments/{id}", updatePaymentViewModel);
                    TempData["Success"] = "Ödeme başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = payment.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ödeme güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var orders = await _apiService.GetAsync<List<OrderViewModel>>("orders");
                var paymentMethods = await _apiService.GetAsync<List<object>>("dropdown/payment-methods");
                var paymentStatuses = await _apiService.GetAsync<List<object>>("dropdown/payment-statuses");
                
                ViewBag.Orders = orders?.Select(o => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Sipariş #{o.Id} - {o.CustomerName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentMethods = paymentMethods?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.PaymentStatuses = paymentStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Orders = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentMethods = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.PaymentStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updatePaymentViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var payment = await _apiService.GetAsync<PaymentViewModel>($"payments/{id}");
                if (payment == null)
                {
                    return NotFound();
                }
                return View(payment);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ödeme bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"payments/{id}");
                TempData["Success"] = "Ödeme başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ödeme silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByOrder(int orderId)
        {
            try
            {
                var payments = await _apiService.GetAsync<List<object>>($"payments/order/{orderId}");
                var viewModels = payments.Select(p => 
                {
                    var payment = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(p));
                    return new PaymentViewModel
                    {
                        Id = payment.GetProperty("id").GetInt32(),
                        OrderId = payment.GetProperty("orderId").GetInt32(),
                        Amount = payment.GetProperty("amount").GetDecimal(),
                        PaymentMethod = payment.GetProperty("paymentMethod").GetString() ?? string.Empty,
                        Status = payment.GetProperty("status").GetString() ?? string.Empty,
                        TransactionId = payment.GetProperty("transactionId").GetString() ?? string.Empty,
                        PaymentDate = payment.GetProperty("paymentDate").GetDateTime(),
                        CreatedDate = payment.GetProperty("createdDate").GetDateTime()
                    };
                }).ToList();

                return View("Index", viewModels);
            }
            catch (Exception)
            {
                TempData["Error"] = "Ödemeler yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByStatus(string status)
        {
            try
            {
                var payments = await _apiService.GetAsync<List<object>>($"payments/status/{status}");
                var viewModels = payments.Select(p => 
                {
                    var payment = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(p));
                    return new PaymentViewModel
                    {
                        Id = payment.GetProperty("id").GetInt32(),
                        OrderId = payment.GetProperty("orderId").GetInt32(),
                        Amount = payment.GetProperty("amount").GetDecimal(),
                        PaymentMethod = payment.GetProperty("paymentMethod").GetString() ?? string.Empty,
                        Status = payment.GetProperty("status").GetString() ?? string.Empty,
                        TransactionId = payment.GetProperty("transactionId").GetString() ?? string.Empty,
                        PaymentDate = payment.GetProperty("paymentDate").GetDateTime(),
                        CreatedDate = payment.GetProperty("createdDate").GetDateTime()
                    };
                }).ToList();

                return View("Index", viewModels);
            }
            catch (Exception)
            {
                TempData["Error"] = "Ödemeler yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var payments = await _apiService.GetAsync<List<object>>($"payments/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                var viewModels = payments.Select(p => 
                {
                    var payment = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(p));
                    return new PaymentViewModel
                    {
                        Id = payment.GetProperty("id").GetInt32(),
                        OrderId = payment.GetProperty("orderId").GetInt32(),
                        Amount = payment.GetProperty("amount").GetDecimal(),
                        PaymentMethod = payment.GetProperty("paymentMethod").GetString() ?? string.Empty,
                        Status = payment.GetProperty("status").GetString() ?? string.Empty,
                        TransactionId = payment.GetProperty("transactionId").GetString() ?? string.Empty,
                        PaymentDate = payment.GetProperty("paymentDate").GetDateTime(),
                        CreatedDate = payment.GetProperty("createdDate").GetDateTime()
                    };
                }).ToList();

                return View("Index", viewModels);
            }
            catch (Exception)
            {
                TempData["Error"] = "Ödemeler yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }

} 