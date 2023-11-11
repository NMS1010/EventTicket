using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
	[Authorize]
	[Route("/profile")]
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}