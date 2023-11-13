using EventTicket.Models;
using EventTicket.Repository.Category;
using EventTicket.Repository.Event;
using EventTicket.Repository.Place;
using EventTicket.Repository.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Authorize(Roles = "0")]
    [Route("/admin/events")]
    public class EventsController : Controller
    {
        private readonly IEventRepository _evRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITopicRepository _topicRepository;

        public EventsController(IEventRepository evRepository, IPlaceRepository placeRepository, ICategoryRepository categoryRepository, ITopicRepository topicRepository)
        {
            _evRepository = evRepository;
            _placeRepository = placeRepository;
            _categoryRepository = categoryRepository;
            _topicRepository = topicRepository;
        }

        public async Task<ActionResult> Index()
        {
            var events = await _evRepository.GetEvents();
            return View(events.ToList());
        }

        [Route("create")]
        public async Task<ActionResult> Create()
        {
            var categories = await _categoryRepository.GetCategories();
            var topics = await _topicRepository.GetTopics();
            var places = await _placeRepository.GetPlaces();

            ViewData["categories"] = categories.Where(x => x.Status).ToList();
            ViewData["topics"] = topics.Where(x => x.Status).ToList();
            ViewData["places"] = places.Where(x => x.Status).ToList();

            return View(new EventVM());
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] EventVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _evRepository.AddEvent(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Không thể tạo mới";
                return View(vm);
            }
        }

        [Route("edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _categoryRepository.GetCategories();
            var topics = await _topicRepository.GetTopics();
            var places = await _placeRepository.GetPlaces();

            ViewData["categories"] = categories.Where(x => x.Status).ToList();
            ViewData["topics"] = topics.Where(x => x.Status).ToList();
            ViewData["places"] = places.Where(x => x.Status).ToList();
            var ev = await _evRepository.GetEvent(id);
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

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromForm] EventVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _evRepository.UpdateEvent(vm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Message"] = "Không thể tạo mới";
                return View(vm);
            }
        }

        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(long id)
        {

            try
            {
                await _evRepository.DeleteEvent(id);

                return RedirectToAction(nameof(Index));
            }
            catch
			{
				return Redirect("/admin/events?error=true");
			}
        }
    }
}