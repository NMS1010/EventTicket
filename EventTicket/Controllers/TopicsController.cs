using EventTicket.Models;
using EventTicket.Repository.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Authorize(Roles = "0")]
    [Route("/admin/topics")]
    public class TopicsController : Controller
    {
        private readonly ITopicRepository _topicRepository;

        public TopicsController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<ActionResult> Index()
        {
            var topics = await _topicRepository.GetTopics();
            return View(topics.ToList());
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View(new TopicVM());
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] TopicVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _topicRepository.AddTopic(vm);

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
            var topic = await _topicRepository.GetTopic(id);
            ViewData["topic"] = topic;
            return View(new TopicVM()
            {
                Id = topic.Id,
                Name = topic.Name,
                Status = topic.Status,
            });
        }

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromForm] TopicVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _topicRepository.UpdateTopic(vm);
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
                await _topicRepository.DeleteTopic(id);

                return RedirectToAction(nameof(Index));
            }
            catch
			{
				return Redirect("/admin/topics?error=true");
			}

        }
    }
}