using EventTicket.Models;
using EventTicket.Repository.User;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Route("/member-register")]
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var success = await _userRepository.Register(vm);
            if (!success)
            {
                ViewData["Message"] = "Thông tin không hợp lệ, tên tài khoản hoặc email đã tồn tại";
                return View(vm);
            }

            return Redirect("/member-login");
        }
    }
}