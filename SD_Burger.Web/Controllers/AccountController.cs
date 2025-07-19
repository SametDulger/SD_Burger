using Microsoft.AspNetCore.Mvc;
using SD_Burger.Web.Models;
using SD_Burger.Web.Services;

namespace SD_Burger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

        public AccountController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _apiService.PostAsync<UserViewModel>("account/login", loginViewModel);
                    if (user != null)
                    {
                        // Burada session yönetimi yapılacak
                        TempData["Success"] = "Başarıyla giriş yaptınız.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Giriş yapılırken hata oluştu: " + ex.Message);
                }
            }

            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            // Burada session temizleme yapılacak
            TempData["Success"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            try
            {
                // Burada mevcut kullanıcının bilgileri alınacak
                var user = await _apiService.GetAsync<UserViewModel>("account/profile");
                if (user == null)
                {
                    return RedirectToAction(nameof(Login));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Profil bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> EditProfile()
        {
            try
            {
                var user = await _apiService.GetAsync<UserViewModel>("account/profile");
                if (user == null)
                {
                    return RedirectToAction(nameof(Login));
                }

                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches ?? new List<BranchViewModel>();
                ViewBag.Roles = roles?.Select(s => new SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<SelectListItem>();

                var editProfileViewModel = new EditProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    BranchId = user.BranchId
                };

                return View(editProfileViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Profil bilgileri yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Profile));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _apiService.PutAsync<UserViewModel>("account/profile", editProfileViewModel);
                    TempData["Success"] = "Profil başarıyla güncellendi.";
                    return RedirectToAction(nameof(Profile));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Profil güncellenirken hata oluştu: " + ex.Message;
                }
            }

            try
            {
                var branches = await _apiService.GetAsync<List<BranchViewModel>>("branches");
                var roles = await _apiService.GetAsync<List<object>>("dropdown/user-roles");
                
                ViewBag.Branches = branches ?? new List<BranchViewModel>();
                ViewBag.Roles = roles?.Select(s => new SelectListItem
                {
                    Value = s.GetType().GetProperty("Value")?.GetValue(s)?.ToString() ?? "",
                    Text = s.GetType().GetProperty("Text")?.GetValue(s)?.ToString() ?? ""
                }).ToList() ?? new List<SelectListItem>();
            }
            catch
            {
                ViewBag.Branches = new List<BranchViewModel>();
                ViewBag.Roles = new List<SelectListItem>();
            }

            return View(editProfileViewModel);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _apiService.PostAsync<object>("account/change-password", changePasswordViewModel);
                    TempData["Success"] = "Şifre başarıyla değiştirildi.";
                    return RedirectToAction(nameof(Profile));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Şifre değiştirilirken hata oluştu: " + ex.Message);
                }
            }

            return View(changePasswordViewModel);
        }
    }
} 