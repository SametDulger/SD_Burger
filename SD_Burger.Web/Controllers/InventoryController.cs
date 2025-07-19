using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Collections.Generic;
using System.Text.Json;

namespace SD_Burger.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IApiService _apiService;

        public InventoryController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var inventories = await _apiService.GetAsync<List<InventoryViewModel>>("inventory");
                return View(inventories ?? new List<InventoryViewModel>());
            }
            catch (Exception)
            {
                TempData["Error"] = "Envanter listesi yüklenirken bir hata oluştu.";
                return View(new List<InventoryViewModel>());
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                
                ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                ViewBag.Branches = branches ?? new List<BranchViewModel>();
                
                return View(new CreateInventoryViewModel());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veri listesi yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Ingredients = new List<IngredientViewModel>();
                ViewBag.Branches = new List<BranchViewModel>();
                return View(new CreateInventoryViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                    var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                    
                    ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                    ViewBag.Branches = branches ?? new List<BranchViewModel>();
                }
                catch
                {
                    ViewBag.Ingredients = new List<IngredientViewModel>();
                    ViewBag.Branches = new List<BranchViewModel>();
                }
                return View(model);
            }

            try
            {
                var inventoryData = new
                {
                    ingredientId = model.IngredientId,
                    branchId = model.BranchId,
                    currentStock = model.CurrentStock
                };

                await _apiService.PostAsync<object>("inventory", inventoryData);
                TempData["Success"] = "Envanter kaydı başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Envanter kaydı oluşturulurken bir hata oluştu.");
                try
                {
                    var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                    var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                    
                    ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                    ViewBag.Branches = branches ?? new List<BranchViewModel>();
                }
                catch
                {
                    ViewBag.Ingredients = new List<IngredientViewModel>();
                    ViewBag.Branches = new List<BranchViewModel>();
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var inventory = await _apiService.GetAsync<InventoryViewModel>($"inventory/{id}");
                return View(inventory);
            }
            catch (Exception)
            {
                TempData["Error"] = "Envanter detayları yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var inventory = await _apiService.GetAsync<InventoryViewModel>($"inventory/{id}");
                
                var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                
                ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                ViewBag.Branches = branches ?? new List<BranchViewModel>();
                
                var viewModel = new UpdateInventoryViewModel
                {
                    IngredientId = inventory.IngredientId,
                    BranchId = inventory.BranchId,
                    CurrentStock = inventory.CurrentStock
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Error"] = "Envanter bilgileri yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                    var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                    
                    ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                    ViewBag.Branches = branches ?? new List<BranchViewModel>();
                }
                catch
                {
                    ViewBag.Ingredients = new List<IngredientViewModel>();
                    ViewBag.Branches = new List<BranchViewModel>();
                }
                return View(model);
            }

            try
            {
                var inventoryData = new
                {
                    ingredientId = model.IngredientId,
                    branchId = model.BranchId,
                    currentStock = model.CurrentStock
                };

                await _apiService.PutAsync<object>($"inventory/{id}", inventoryData);
                TempData["Success"] = "Envanter kaydı başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Envanter kaydı güncellenirken bir hata oluştu.");
                try
                {
                    var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                    var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                    
                    ViewBag.Ingredients = ingredients ?? new List<IngredientViewModel>();
                    ViewBag.Branches = branches ?? new List<BranchViewModel>();
                }
                catch
                {
                    ViewBag.Ingredients = new List<IngredientViewModel>();
                    ViewBag.Branches = new List<BranchViewModel>();
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var inventory = await _apiService.GetAsync<InventoryViewModel>($"inventory/{id}");
                return View(inventory);
            }
            catch (Exception)
            {
                TempData["Error"] = "Envanter bilgileri yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"inventory/{id}");
                TempData["Success"] = "Envanter kaydı başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Envanter kaydı silinirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByIngredient(int ingredientId)
        {
            try
            {
                var inventories = await _apiService.GetAsync<List<InventoryViewModel>>($"inventory/ingredient/{ingredientId}");
                return View("Index", inventories ?? new List<InventoryViewModel>());
            }
            catch (Exception)
            {
                TempData["Error"] = "Malzeme envanteri yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetLowStock(decimal threshold = 10)
        {
            try
            {
                var inventories = await _apiService.GetAsync<List<InventoryViewModel>>($"inventory/lowstock/{threshold}");
                return View("Index", inventories ?? new List<InventoryViewModel>());
            }
            catch (Exception)
            {
                TempData["Error"] = "Düşük stok envanteri yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 