using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Collections.Generic;

namespace SD_Burger.Web.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IApiService _apiService;

        public BranchesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                return View(branches);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şubeler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<BranchViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var branch = await _apiService.GetAsync<BranchViewModel>($"branches/{id}");
                if (branch == null)
                {
                    return NotFound();
                }
                return View(branch);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şube detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBranchViewModel createBranchViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var branch = await _apiService.PostAsync<BranchViewModel>("branches", createBranchViewModel);
                    TempData["Success"] = "Şube başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = branch.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Şube oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            return View(createBranchViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var branch = await _apiService.GetAsync<BranchViewModel>($"branches/{id}");
                if (branch == null)
                {
                    return NotFound();
                }

                var updateBranchViewModel = new UpdateBranchViewModel
                {
                    Name = branch.Name,
                    Address = branch.Address,
                    PhoneNumber = branch.PhoneNumber,
                    Email = branch.Email,
                    TableCount = branch.TableCount,
                    IsActive = branch.IsActive
                };

                return View(updateBranchViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şube bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateBranchViewModel updateBranchViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var branch = await _apiService.PutAsync<BranchViewModel>($"branches/{id}", updateBranchViewModel);
                    TempData["Success"] = "Şube başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = branch.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Şube güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(updateBranchViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var branch = await _apiService.GetAsync<BranchViewModel>($"branches/{id}");
                if (branch == null)
                {
                    return NotFound();
                }
                return View(branch);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şube bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"branches/{id}");
                TempData["Success"] = "Şube başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şube silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetActiveBranches()
        {
            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches/active");
                return View("Index", branches);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Aktif şubeler yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 