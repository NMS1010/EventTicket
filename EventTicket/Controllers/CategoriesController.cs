using EventTicket.Entities;
using EventTicket.Models.Category;
using EventTicket.Repository.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.Controllers
{
    [Authorize(Roles = "0")]
    [Route("/admin/categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await _categoryRepository.GetCategories();
            return View(categories.ToList());
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View(new CategoryVM());
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] CategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _categoryRepository.AddCategory(vm);

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
            var category = await _categoryRepository.GetCategory(id);
            ViewData["cate"] = category;
            return View(new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                Status = category.Status,
                Image = null,
            });
        }

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromForm] CategoryVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Dữ liệu không hợp lệ";
                return View(vm);
            }
            try
            {
                await _categoryRepository.UpdateCategory(vm);
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
                await _categoryRepository.DeleteCategory(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Redirect("/admin/categories?error=true");
            }
        }
    }
}