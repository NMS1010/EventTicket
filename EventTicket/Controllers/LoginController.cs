using EventTicket.Entities;
using EventTicket.Models;
using EventTicket.Repository.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EventTicket.Controllers
{
	[Route("/member-login")]
	public class LoginController : Controller
	{
		private readonly IUserRepository _userRepository;

		public LoginController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		private async Task AssignCookies(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
			};

			var identity = new ClaimsIdentity(claims, "UserAuth");

			var userPrincipal = new ClaimsPrincipal(identity);
			var authProperties = new AuthenticationProperties()
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
			};
			await HttpContext.SignInAsync(
				"UserAuth",
				userPrincipal,
				authProperties
			);
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			var user = await _userRepository.Login(vm);
			if (user == null)
			{
				ViewData["Message"] = "Tài khoản hoặc mật khẩu không chính xác";
				return View(vm);
			}

			var obj = JsonConvert.SerializeObject(user);
			HttpContext.Session.SetString("User", obj);
			HttpContext.Session.SetString("UserId", user.Id.ToString());
			HttpContext.Session.SetString("Name", user.Name.ToString());
			HttpContext.Session.SetString("Avatar", user.Avatar.ToString());
			await AssignCookies(user);

			if (user.Role == 0)
			{
				return Redirect("/admin/categories");
			}
			return Redirect("/");
		}
	}
}