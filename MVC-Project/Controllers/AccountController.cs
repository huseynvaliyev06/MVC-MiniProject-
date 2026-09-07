using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services.Interfaces;
using MVC_MiniProject.ViewModels.Account;
using System.Threading.Tasks;

namespace MVC_MiniProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        // ── REGISTER ──────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var existUser = await _userManager.FindByEmailAsync(vm.Email);
            if (existUser != null)
            {
                ModelState.AddModelError("Email", "Bu email artıq istifadə olunur.");
                return View(vm);
            }

            var user = new AppUser
            {
                FullName = vm.FullName,
                UserName = vm.UserName,
                Email = vm.Email,
                EmailConfirmed = false // Email təsdiqi gözlənilir
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            // Email təsdiq tokeni yarat və link göndər
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmLink = Url.Action(
                "ConfirmEmail", "Account",
                new { userId = user.Id, token = token },
                Request.Scheme);

            var html = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 8px;'>
                    <h2 style='color: #333;'>eLEARN Platformasına Xoş Gəlmisiniz!</h2>
                    <p style='color: #555;'>Hesabınızı təsdiqləmək üçün aşağıdakı düyməyə klikləyin:</p>
                    <a href='{confirmLink}' style='display: inline-block; background-color: #f67f00; color: #ffffff; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                        Emaili Təsdiqlə
                    </a>
                    <p style='margin-top: 20px; color: #888; font-size: 13px;'>Əgər bu qeydiyyatı siz etməmisinizsə, bu məktubu nəzərə almayın.</p>
                </div>";

            await _emailService.SendAsync(user.Email, "Email Təsdiqi — eLEARN", html);

            TempData["Info"] = "Qeydiyyat uğurla tamamlandı! Zəhmət olmasa email qutunuzu yoxlayın və hesabınızı təsdiqləyin.";
            return RedirectToAction("Login");
        }

        // ── CONFIRM EMAIL ─────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Təsdiq linki yanlışdır və ya vaxtı bitib.";
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "İstifadəçi tapılmadı.";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Email təsdiqlənməsi uğursuz oldu. Link köhnəlmiş ola bilər.";
                return RedirectToAction("Login");
            }

            TempData["Success"] = "Emailiniz uğurla təsdiqləndi! Artıq sistemə daxil ola bilərsiniz.";
            return RedirectToAction("Login");
        }

        // ── LOGIN ─────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır.");
                return View(vm);
            }

            // Email təsdiqlənməyibsə giriş qadağandır
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Zəhmət olmasa əvvəlcə emailinizə göndərilən linklə hesabınızı təsdiq edin.");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, vm.Password, vm.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır.");
                return View(vm);
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // Rola görə yönləndirmə:
            if (await _userManager.IsInRoleAsync(user, "SuperAdmin") ||
                await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        // ── LOGOUT ────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // ── ACCESS DENIED ─────────────────────────────────────────────────
        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}