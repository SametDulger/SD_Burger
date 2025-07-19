using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;

namespace SD_Burger.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IApiService _apiService;

        public ReservationsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>("reservations");
                return View(reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<ReservationViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _apiService.GetAsync<ReservationViewModel>($"reservations/{id}");
                if (reservation == null)
                {
                    return NotFound();
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyon detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                var reservationStatuses = await _apiService.GetAsync<List<object>>("dropdown/reservation-statuses");

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
                
                ViewBag.Users = users?.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.ReservationStatuses = reservationStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
                ViewBag.Users = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.ReservationStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationViewModel createReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = await _apiService.PostAsync<ReservationViewModel>("reservations", createReservationViewModel);
                    TempData["Success"] = "Rezervasyon başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Rezervasyon oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                var reservationStatuses = await _apiService.GetAsync<List<object>>("dropdown/reservation-statuses");

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
                
                ViewBag.Users = users?.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.ReservationStatuses = reservationStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Tables = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Users = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.ReservationStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createReservationViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var reservation = await _apiService.GetAsync<ReservationViewModel>($"reservations/{id}");
                if (reservation == null)
                {
                    return NotFound();
                }

                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                var reservationStatuses = await _apiService.GetAsync<List<object>>("dropdown/reservation-statuses");

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
                
                ViewBag.Users = users?.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.ReservationStatuses = reservationStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

                var updateReservationViewModel = new UpdateReservationViewModel
                {
                    ReservationDate = reservation.ReservationDate ?? DateTime.Today,
                    ReservationTime = reservation.ReservationTime ?? TimeSpan.Zero,
                    GuestCount = reservation.GuestCount,
                    CustomerName = reservation.CustomerName,
                    CustomerPhone = reservation.CustomerPhone,
                    CustomerEmail = reservation.CustomerEmail,
                    SpecialRequests = reservation.SpecialRequests,
                    Status = reservation.Status,
                    TableId = reservation.TableId,
                    BranchId = reservation.BranchId,
                    UserId = reservation.UserId
                };

                return View(updateReservationViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyon bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateReservationViewModel updateReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = await _apiService.PutAsync<ReservationViewModel>($"reservations/{id}", updateReservationViewModel);
                    TempData["Success"] = "Rezervasyon başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = reservation.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Rezervasyon güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                var reservationStatuses = await _apiService.GetAsync<List<object>>("dropdown/reservation-statuses");

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
                
                ViewBag.Users = users?.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.ReservationStatuses = reservationStatuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Tables = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Users = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.ReservationStatuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updateReservationViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _apiService.GetAsync<ReservationViewModel>($"reservations/{id}");
                if (reservation == null)
                {
                    return NotFound();
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyon bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"reservations/{id}");
                TempData["Success"] = "Rezervasyon başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyon silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByBranch(int branchId)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/branch/{branchId}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByTable(int tableId)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/table/{tableId}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByStatus(string status)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/status/{status}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByDate(DateTime date)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/date/{date:yyyy-MM-dd}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByUser(int userId)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/user/{userId}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByCustomer(string customerName)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/customer/{customerName}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByGuestCount(int guestCount)
        {
            try
            {
                var reservations = await _apiService.GetAsync<List<ReservationViewModel>>($"reservations/guestcount/{guestCount}");
                return View("Index", reservations);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Rezervasyonlar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetAvailableTables(int branchId, DateTime date, TimeSpan time, int guestCount)
        {
            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>($"reservations/available-tables?branchId={branchId}&date={date:yyyy-MM-dd}&time={time}&guestCount={guestCount}");
                return Json(tables);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }

} 