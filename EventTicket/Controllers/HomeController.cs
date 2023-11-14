using EventTicket.Models;
using EventTicket.Repository.Category;
using EventTicket.Repository.Event;
using EventTicket.Repository.Place;
using EventTicket.Repository.Topic;
using EventTicket.Repository.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventTicket.Controllers
{
	public class HomeController : Controller
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ITopicRepository topicRepository, IEventRepository eventRepository, ICategoryRepository categoryRepository, IPlaceRepository placeRepository, IUserRepository userRepository)
        {
            _topicRepository = topicRepository;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _placeRepository = placeRepository;
            _userRepository = userRepository;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var topics = await _topicRepository.GetTopics();
            var events = await _eventRepository.GetEvents();

            ViewData["topics"] = topics;
            ViewData["events"] = events;

            return View();
        }

        [Route("chu-de-su-kien")]
        public async Task<IActionResult> Topic()
        {
            var topics = await _topicRepository.GetTopics();
            var events = await _eventRepository.GetEvents();

            ViewData["topics"] = topics.Where(x => x.Status).ToList(); ;
            ViewData["events"] = events.Where(x => x.Status != "Ẩn").ToList();

            return View("Topics");
        }

        [Route("danh-muc-su-kien")]
        public async Task<IActionResult> Category()
        {
            var categories = await _categoryRepository.GetCategories();
            var events = await _eventRepository.GetEvents();

            ViewData["categories"] = categories.Where(x => x.Status).ToList();
            ViewData["events"] = events.Where(x => x.Status != "Ẩn").ToList();

            return View("Categories");
        }

        [Route("dia-diem-to-chuc-su-kien")]
        public async Task<IActionResult> Place()
        {
            var places = await _placeRepository.GetPlaces();
            var events = await _eventRepository.GetEvents();

            ViewData["places"] = places.Where(x => x.Status).ToList();
            ViewData["events"] = events.Where(x => x.Status != "Ẩn").ToList();

            return View("Places");
        }

        [Route("danh-sach-su-kien")]
        public async Task<IActionResult> Event()
        {
            var events = await _eventRepository.GetEvents();

            ViewData["events"] = events.Where(x => x.Status != "Ẩn").ToList();

            return View("Events");
        }

        [Route("su-kien/{id}")]
        public async Task<IActionResult> EventDetail(long id)
        {
            var events = await _eventRepository.GetEvents();
            ViewData["events"] = events.Where(x => x.Status != "Ẩn").ToList();
            var ev = events.FirstOrDefault(x => x.Id == id);

            return View("EventDetail", ev);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("Avatar");
            HttpContext.Session.Remove("Name");
            await HttpContext.SignOutAsync("UserAuth");

            return Redirect("/");
        }

		[Authorize]
		[Route("member-account")]
        public async Task<IActionResult> MemberAccount()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return Redirect("/home/logout");
            var user = await _userRepository.GetById(long.Parse(userId));
            return View("Account", user);
        }
    }
}