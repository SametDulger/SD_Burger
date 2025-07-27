using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Web.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IApiService _apiService;

        public IngredientsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients");
                return View(ingredients);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Malzemeler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<IngredientViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var ingredient = await _apiService.GetAsync<IngredientViewModel>($"ingredients/{id}");
                if (ingredient == null)
                {
                    return NotFound();
                }
                return View(ingredient);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Malzeme detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            try
            {
                var units = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                {
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Kilogram" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "1", Text = "Gram" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "2", Text = "Litre" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "3", Text = "Mililitre" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "4", Text = "Adet" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "5", Text = "Paket" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "6", Text = "Kutu" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "7", Text = "Şişe" }
                };
                ViewBag.Units = units;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Units = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateIngredientViewModel createIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ingredient = await _apiService.PostAsync<IngredientViewModel>("ingredients", createIngredientViewModel);
                    TempData["Success"] = "Malzeme başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = ingredient.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Malzeme oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var units = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                {
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Kilogram" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "1", Text = "Gram" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "2", Text = "Litre" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "3", Text = "Mililitre" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "4", Text = "Adet" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "5", Text = "Paket" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "6", Text = "Kutu" },
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "7", Text = "Şişe" }
                };
                ViewBag.Units = units;
            }
            catch
            {
                ViewBag.Units = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createIngredientViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var ingredient = await _apiService.GetAsync<IngredientViewModel>($"ingredients/{id}");
                if (ingredient == null)
                {
                    return NotFound();
                }

                var units = await _apiService.GetAsync<List<object>>("dropdown/ingredient-units");
                ViewBag.Units = units ?? new List<object>();

                var updateIngredientViewModel = new UpdateIngredientViewModel
                {
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    Unit = ingredient.Unit,
                    UnitPrice = ingredient.UnitPrice,
                    MinimumStock = ingredient.MinimumStock
                };

                return View(updateIngredientViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Malzeme bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateIngredientViewModel updateIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ingredient = await _apiService.PutAsync<IngredientViewModel>($"ingredients/{id}", updateIngredientViewModel);
                    TempData["Success"] = "Malzeme başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = ingredient.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Malzeme güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var units = await _apiService.GetAsync<List<object>>("dropdown/ingredient-units");
                ViewBag.Units = units ?? new List<object>();
            }
            catch
            {
                ViewBag.Units = new List<object>();
            }

            return View(updateIngredientViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ingredient = await _apiService.GetAsync<IngredientViewModel>($"ingredients/{id}");
                if (ingredient == null)
                {
                    return NotFound();
                }
                return View(ingredient);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Malzeme bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"ingredients/{id}");
                TempData["Success"] = "Malzeme başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Malzeme silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetActiveIngredients()
        {
            try
            {
                var ingredients = await _apiService.GetAsync<List<IngredientViewModel>>("ingredients/active");
                return View("Index", ingredients);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Aktif malzemeler yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }

} 