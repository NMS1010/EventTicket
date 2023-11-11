using EventTicket.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Authorize(Roles = "0")]
    [Route("/admin/users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAll();
            return View(users);
        }
        [Route("toggle/{id}")]
        public async Task<IActionResult> Toggle(long id)
        {
            var res = await _userRepository.Toggle(id);
            var url = "/admin/users" + (res ? "" : "?error=true");

            return Redirect(url);
        }
    }
}