using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;

namespace SD_Burger.Web.Controllers
{
    public class TablesController : Controller
    {
        private readonly IApiService _apiService;

        public TablesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var tables = await _apiService.GetAsync<List<TableViewModel>>("tables");
                return View(tables);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Masalar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<TableViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var table = await _apiService.GetAsync<TableViewModel>($"tables/{id}");
                if (table == null)
                {
                    return NotFound();
                }
                return View(table);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Masa detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var statuses = await _apiService.GetAsync<List<object>>("dropdown/table-statuses");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Statuses = statuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Statuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTableViewModel createTableViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var table = await _apiService.PostAsync<TableViewModel>("tables", createTableViewModel);
                    TempData["Success"] = "Masa başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = table.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Masa oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var statuses = await _apiService.GetAsync<List<object>>("dropdown/table-statuses");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Statuses = statuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Statuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createTableViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var table = await _apiService.GetAsync<TableViewModel>($"tables/{id}");
                if (table == null)
                {
                    return NotFound();
                }

                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var statuses = await _apiService.GetAsync<List<object>>("dropdown/table-statuses");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Statuses = statuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

                var updateTableViewModel = new UpdateTableViewModel
                {
                    TableNumber = table.TableNumber,
                    Capacity = table.Capacity,
                    Status = table.Status,
                    BranchId = table.BranchId
                };

                return View(updateTableViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Masa bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTableViewModel updateTableViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var table = await _apiService.PutAsync<TableViewModel>($"tables/{id}", updateTableViewModel);
                    TempData["Success"] = "Masa başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = table.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Masa güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var statuses = await _apiService.GetAsync<List<object>>("dropdown/table-statuses");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Statuses = statuses?.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Statuses = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updateTableViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var table = await _apiService.GetAsync<TableViewModel>($"tables/{id}");
                if (table == null)
                {
                    return NotFound();
                }
                return View(table);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Masa bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"tables/{id}");
                TempData["Success"] = "Masa başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Masa silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 