using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;
using System.Collections.Generic;

namespace SD_Burger.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IApiService _apiService;

        public UsersController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users");
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<UserViewModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>($"users/{id}");
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı detayları yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Roles = roles?.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.GetType().GetProperty("Value")?.GetValue(r)?.ToString() ?? "",
                    Text = r.GetType().GetProperty("Text")?.GetValue(r)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Roles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _apiService.PostAsync<UserViewModel>("users", createUserViewModel);
                    TempData["Success"] = "Kullanıcı başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Details), new { id = user.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kullanıcı oluşturulurken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Roles = roles?.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.GetType().GetProperty("Value")?.GetValue(r)?.ToString() ?? "",
                    Text = r.GetType().GetProperty("Text")?.GetValue(r)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Roles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(createUserViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>($"users/{id}");
                if (user == null)
                {
                    return NotFound();
                }

                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Roles = roles?.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.GetType().GetProperty("Value")?.GetValue(r)?.ToString() ?? "",
                    Text = r.GetType().GetProperty("Text")?.GetValue(r)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

                var updateUserViewModel = new UpdateUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    BranchId = user.BranchId
                };

                return View(updateUserViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateUserViewModel updateUserViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _apiService.PutAsync<UserViewModel>($"users/{id}", updateUserViewModel);
                    TempData["Success"] = "Kullanıcı başarıyla güncellendi.";
                    return RedirectToAction(nameof(Details), new { id = user.Id });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kullanıcı güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches?.Select(b => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                
                ViewBag.Roles = roles?.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = r.GetType().GetProperty("Value")?.GetValue(r)?.ToString() ?? "",
                    Text = r.GetType().GetProperty("Text")?.GetValue(r)?.ToString() ?? ""
                }).ToList() ?? new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }
            catch
            {
                ViewBag.Branches = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                ViewBag.Roles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            }

            return View(updateUserViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>($"users/{id}");
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _apiService.DeleteAsync($"users/{id}");
                TempData["Success"] = "Kullanıcı başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByBranch(int branchId)
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>($"users/branch/{branchId}");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByRole(string role)
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>($"users/role/{role}");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users/active");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetInactiveUsers()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users/inactive");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>($"users/search/{searchTerm}");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetWaiters()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users/waiters");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetKitchenStaff()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users/kitchen-staff");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetAdmins()
        {
            try
            {
                var users = await _apiService.GetAsync<List<UserViewModel>>("users/admins");
                return View("Index", users);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>($"users/email/{email}");
                if (user == null)
                {
                    return NotFound();
                }
                return View("Details", user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>($"users/username/{username}");
                if (user == null)
                {
                    return NotFound();
                }
                return View("Details", user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcı bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }

} 