using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ProductManagementMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email và mật khẩu không được để trống");
                return View();
            }

            var account = _accountService.Authenticate(email, password);
            if (account != null)
            {
                CurrentUser = account;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountMember account)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại chưa
                var existingAccount = _accountService.GetAccountByEmail(account.EmailAddress);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("EmailAddress", "Email đã được sử dụng");
                    return View(account);
                }

                // Tạo ID mới
                account.MemberId = Guid.NewGuid().ToString();
                // Mặc định role là user (2)
                account.MemberRole = 2;

                _accountService.SaveAccount(account);
                TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }

            return View(account);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // Admin only - Quản lý tài khoản
        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            return View();
        }

        [HttpPost]
        public IActionResult Create(AccountMember account)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                var existingAccount = _accountService.GetAccountByEmail(account.EmailAddress);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("EmailAddress", "Email đã được sử dụng");
                    return View(account);
                }

                account.MemberId = Guid.NewGuid().ToString();
                _accountService.SaveAccount(account);
                TempData["Success"] = "Tạo tài khoản thành công!";
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(AccountMember account)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            if (ModelState.IsValid)
            {
                _accountService.UpdateAccount(account);
                TempData["Success"] = "Cập nhật tài khoản thành công!";
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            if (!IsAdmin)
                return RedirectToAccessDenied();

            _accountService.DeleteAccount(id);
            TempData["Success"] = "Xóa tài khoản thành công!";
            return RedirectToAction("Index");
        }
    }
} 