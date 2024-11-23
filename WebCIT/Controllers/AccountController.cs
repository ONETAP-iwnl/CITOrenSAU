using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCIT.Api.AuthService;
using WebCIT.Classed.Ticket;
using WebCIT.Models;

namespace WebCIT.Controllers
{

    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController()
        {
            _authService = new AuthService("https://0.0.0.0:7118");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userResponse = await _authService.LoginAsync(model.Login, model.Password);

                if (userResponse != null)
                {
                    switch (userResponse.Role)
                    {
                        case "Администратор": //роль у нас не АДМИН, А Администратор, НИКИТА!!!!
                            return RedirectToAction("AdminDashboard", "Admin");

                        case "Тех. Специалист":
                            return RedirectToAction("TechDashboard", "Tech");

                        case "Сотрудник":
                            return RedirectToAction("EmployeeDashboard", "Employee");

                        default:
                            ModelState.AddModelError(string.Empty, "У вас нет прав доступа.");
                            return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Логин или пароль неверны.");
                }
            }

            return View(model);
        }
    }
}
