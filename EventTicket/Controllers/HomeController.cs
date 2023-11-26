using EventTicket.Models;
using EventTicket.Repository.Category;
using EventTicket.Repository.Event;
using EventTicket.Repository.Place;
using EventTicket.Repository.Topic;
using EventTicket.Repository.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [Route("member-account/event/create")]
        public async Task<IActionResult> AddEvent()
        {
            var categories = await _categoryRepository.GetCategories();
            var topics = await _topicRepository.GetTopics();
            var places = await _placeRepository.GetPlaces();

            ViewData["categories"] = categories.Where(x => x.Status).ToList();
            ViewData["topics"] = topics.Where(x => x.Status).ToList();
            ViewData["places"] = places.Where(x => x.Status).ToList();

            return View(new EventVM());
        }

        [Authorize]
        [HttpPost("member-account/event/create")]
        public async Task<ActionResult> AddEvent([FromForm] EventVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _eventRepository.AddEvent(vm);

                return Redirect("/member-account");
            }
            catch
            {
                ViewData["Message"] = "Không thể tạo mới";
                return View(vm);
            }
        }

        [Authorize]
        [Route("member-account/event/edit/{id}")]
        public async Task<ActionResult> UpdateEvent(int id)
        {
            var categories = await _categoryRepository.GetCategories();
            var topics = await _topicRepository.GetTopics();
            var places = await _placeRepository.GetPlaces();

            ViewData["categories"] = categories.Where(x => x.Status).ToList();
            ViewData["topics"] = topics.Where(x => x.Status).ToList();
            ViewData["places"] = places.Where(x => x.Status).ToList();
            var ev = await _eventRepository.GetEvent(id);
            ViewData["event"] = ev;

            return View(new EventVM()
            {
                Id = ev.Id,
                Name = ev.Name,
                Status = ev.Status,
                Image = null,
                CategoryId = ev.Category.Id,
                Description = ev.Description,
                EndDate = ev.EndDate,
                PlaceId = ev.Place.Id,
                StartDate = ev.StartDate,
                TopicId = ev.Topic.Id,
                Organizer = ev.Organizer,
            });
        }

        [Authorize]
        [Route("member-account/event/detail/{id}")]
        public async Task<ActionResult> DetailEvent(int id)
        {
            if (id < 1)
                return Redirect("/member-account");
            var ev = await _eventRepository.GetEvent(id);

            return View(ev);
        }

        [Authorize]
        [HttpPost("member-account/event/edit")]
        public async Task<ActionResult> UpdateEvent([FromForm] EventVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _eventRepository.UpdateEvent(vm);
                return Redirect("/member-account");
            }
            catch
            {
                ViewData["Message"] = "Không thể tạo mới";
                return View(vm);
            }
        }
    }
}