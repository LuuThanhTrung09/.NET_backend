using Game106_API.Models;
using Game106_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game106_API.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly IEmailServices _emailServices;
        private readonly ICharacterServices _characterServices;

        public AdminController(IAccountServices accountServices, IEmailServices emailServices, ICharacterServices characterServices)
        {
            _accountServices = accountServices;
            _emailServices = emailServices;
            _characterServices = characterServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Account account)
        {
            try
            {
                if (string.IsNullOrEmpty(account.Email) || string.IsNullOrEmpty(account.Password))
                    throw new Exception("Email, password or name must be not empty");

                var result = await _accountServices.Login(account.Email, account.Password);
                if (result == null)
                    throw new Exception("Email, password or name must be not empty");

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return View(account);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account account)
        {
            try
            {
                // 1. Validate
                if (string.IsNullOrEmpty(account.Email) || string.IsNullOrEmpty(account.Password) || string.IsNullOrEmpty(account.Name))
                    throw new Exception("Email, password or name must be not empty");

                // 2. Lưu vào database
                var result = await _accountServices.Register(account.Email, account.Password, account.Name);

                TempData["SuccessMessage"] = $"Tạo tài khoản thành công! Email xác nhận đã được gửi đến {account.Email}.";
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ViewBag.ErrorMessage = e.Message;
                return View(account);
            }
        }

        public async Task<IActionResult> ListAccount()
        {
            try
            {
                var accounts = await _accountServices.GetAllAccounts();
                return View(accounts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        public async Task<IActionResult> ListCharacter()
        {
            try
            {
                var characters = await _characterServices.GetAllCharacters();
                return View(characters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    }
}