using EventTicket.Models;
using EventTicket.Repository.Place;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Authorize(Roles = "0")]
    [Route("/admin/places")]
    public class PlacesController : Controller
    {
        private readonly IPlaceRepository _placeRepository;

        public PlacesController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<ActionResult> Index()
        {
            var places = await _placeRepository.GetPlaces();
            return View(places.ToList());
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View(new PlaceVM());
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] PlaceVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _placeRepository.AddPlace(vm);

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
            var place = await _placeRepository.GetPlace(id);
            ViewData["place"] = place;
            return View(new PlaceVM()
            {
                Id = place.Id,
                Name = place.Name,
                Status = place.Status,
                CreatedBy = place.CreatedBy,
                Address = place.Address,
                Description = place.Description,
                Lat = place.Lat,
                Long = place.Long,
                Phone = place.Phone,
                TimeActive = place.TimeActive,
                PlaceId = place.PlaceId,
            });
        }

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromForm] PlaceVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _placeRepository.UpdatePlace(vm);
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
                await _placeRepository.DeletePlace(id);

                return RedirectToAction(nameof(Index));
            }
            catch
			{
				return Redirect("/admin/places?error=true");
			}

        }
    }
}