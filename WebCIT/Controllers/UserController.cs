using Microsoft.AspNetCore.Mvc;
using WebCIT.Api.UserService;
using WebCIT.Models;

namespace WebCIT.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService("https://26.240.38.124:5235");
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _userService.RegisterUserToMicroservice(model);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Пользователь успешно зарегистрирован.";
                    return RedirectToAction("AdminDashboard", "Ticket");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка регистрации пользователя. Попробуйте снова.");
                    Console.WriteLine(isSuccess.ToString());
                }
                
            }
            else
            {
                Console.WriteLine("Model state is not valid");
            }

            return View(model); // Вернуть форму с ошибками валидации
        }
    }
}