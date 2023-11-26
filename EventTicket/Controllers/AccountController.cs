using EventTicket.Models;
using EventTicket.Repository.Event;
using EventTicket.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IEventRepository _eventRepository;

		public AccountController(IUserRepository userRepository, IEventRepository eventRepository)
		{
			_userRepository = userRepository;
			_eventRepository = eventRepository;
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAccountInfo([FromForm] UserEditVM vm)
		{
			await _userRepository.EditInfoUser(vm);
			return Redirect("/member-account");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAccountAvatar([FromForm] UserEditAvatar vm)
		{
			await _userRepository.EditAvatarUser(vm);
			return Redirect("/member-account");
		}

		[HttpPost]
		public async Task<IActionResult> RegisterEvent([FromForm] RegisterEventVM vm)
		{
			var userId = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userId))
				return Redirect("/member-login");

			vm.UserId = long.Parse(userId);
			await _userRepository.RegisterEvent(vm);

			return Redirect("/su-kien/" + vm.EventId + "?order=true");
		}
	}
}