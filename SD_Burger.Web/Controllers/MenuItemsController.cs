using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Collections.Generic;

namespace SD_Burger.Web.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IApiService _apiService;

        public MenuItemsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var menuItems = await _apiService.GetAsync<List<MenuItemViewModel>>("menuitems");
                return View(menuItems);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğeleri yüklenirken hata oluştu: " + ex.Message;
                return View(new List<MenuItemViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var menuItem = await _apiService.GetAsync<MenuItemViewModel>($"menuitems/{id}");
                if (menuItem == null)
                {
                    return NotFound();
                }
                return View(menuItem);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğesi detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories");
                var selectList = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Categories = selectList;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kategori listesi yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Categories = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMenuItemViewModel createMenuItemViewModel, IFormFile imageFile)
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
                            return View(createMenuItemViewModel);
                        }

                        // Dosya tipi kontrolü
                        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
                        if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
                        {
                            TempData["Error"] = "Sadece JPG, JPEG ve PNG dosyaları kabul edilir!";
                            return View(createMenuItemViewModel);
                        }

                        // Base64'e çevir
                        using var memoryStream = new MemoryStream();
                        await imageFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        createMenuItemViewModel.ImageUrl = $"data:{imageFile.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
                    }

                    var menuItem = await _apiService.PostAsync<MenuItemViewModel>("menuitems", createMenuItemViewModel);
                    TempData["Success"] = "Menü öğesi başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = menuItem.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Menü öğesi oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories");
                var selectList = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Categories = selectList;
            }
            catch
            {
                ViewBag.Categories = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createMenuItemViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var menuItem = await _apiService.GetAsync<MenuItemViewModel>($"menuitems/{id}");
                if (menuItem == null)
                {
                    return NotFound();
                }

                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories");
                var selectList = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Categories = selectList;

                var updateMenuItemViewModel = new UpdateMenuItemViewModel
                {
                    Name = menuItem.Name,
                    Description = menuItem.Description,
                    Price = menuItem.Price,
                    ImageUrl = menuItem.ImageUrl,
                    IsAvailable = menuItem.IsAvailable,
                    CategoryId = menuItem.CategoryId
                };

                return View(updateMenuItemViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğesi bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateMenuItemViewModel updateMenuItemViewModel, IFormFile imageFile)
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
                            return View(updateMenuItemViewModel);
                        }

                        // Dosya tipi kontrolü
                        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
                        if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
                        {
                            TempData["Error"] = "Sadece JPG, JPEG ve PNG dosyaları kabul edilir!";
                            return View(updateMenuItemViewModel);
                        }

                        // Base64'e çevir
                        using var memoryStream = new MemoryStream();
                        await imageFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        updateMenuItemViewModel.ImageUrl = $"data:{imageFile.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
                    }

                    var menuItem = await _apiService.PutAsync<MenuItemViewModel>($"menuitems/{id}", updateMenuItemViewModel);
                    TempData["Success"] = "Menü öğesi başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = menuItem.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Menü öğesi güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var categories = await _apiService.GetAsync<List<CategoryViewModel>>("categories");
                var selectList = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Categories = selectList;
            }
            catch
            {
                ViewBag.Categories = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updateMenuItemViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var menuItem = await _apiService.GetAsync<MenuItemViewModel>($"menuitems/{id}");
                if (menuItem == null)
                {
                    return NotFound();
                }
                return View(menuItem);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğesi bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"menuitems/{id}");
                TempData["Success"] = "Menü öğesi başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğesi silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            try
            {
                var menuItems = await _apiService.GetAsync<List<MenuItemViewModel>>($"menuitems/category/{categoryId}");
                return View("Index", menuItems);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Menü öğeleri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetAvailableItems()
        {
            try
            {
                var menuItems = await _apiService.GetAsync<List<MenuItemViewModel>>("menuitems/available");
                return View("Index", menuItems);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mevcut menü öğeleri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 