using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IApiService _apiService;

        public CategoriesController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories");
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Kategoriler yüklenirken hata oluştu: {ex.Message}";
                return View(new List<CategoryViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Resim dosyası işleme
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Dosya boyutu kontrolü (5MB)
                        if (imageFile.Length > 5 * 1024 * 1024)
                        {
                            TempData["Error"] = "Dosya boyutu 5MB'dan büyük olamaz!";
                            return View(model);
                        }

                        // Dosya tipi kontrolü
                        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
                        if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
                        {
                            TempData["Error"] = "Sadece JPG, JPEG ve PNG dosyaları kabul edilir!";
                            return View(model);
                        }

                        // Base64'e çevir
                        using var memoryStream = new MemoryStream();
                        await imageFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        model.ImageUrl = $"data:{imageFile.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
                    }

                    await _apiService.PostAsync<CategoryViewModel>("categories", model);
                    TempData["Success"] = "Kategori başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Kategori oluşturulurken hata oluştu: {ex.Message}";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _apiService.GetAsync<CategoryViewModel>($"categories/{id}");
                if (category == null)
                {
                    TempData["Error"] = "Kategori bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Kategori detayları yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _apiService.GetAsync<CategoryViewModel>($"categories/{id}");
                if (category == null)
                {
                    TempData["Error"] = "Kategori bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                var updateModel = new UpdateCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ImageUrl = category.ImageUrl,
                    DisplayOrder = category.DisplayOrder,
                    IsActive = category.IsActive
                };

                return View(updateModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Kategori düzenleme sayfası yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _apiService.PutAsync<CategoryViewModel>($"categories/{id}", model);
                    TempData["Success"] = "Kategori başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Kategori güncellenirken hata oluştu: {ex.Message}";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _apiService.GetAsync<CategoryViewModel>($"categories/{id}");
                if (category == null)
                {
                    TempData["Error"] = "Kategori bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Kategori silme sayfası yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"categories/{id}");
                TempData["Success"] = "Kategori başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Kategori silinirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetActiveCategories()
        {
            try
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories/active");
                return View("Index", categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Aktif kategoriler yüklenirken hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 