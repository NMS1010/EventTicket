using EventTicket.Models.Category;
using EventTicket.Repository.DBContext;
using EventTicket.Services;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IUploadService _uploadService;

        public CategoryRepository(AppDbContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task AddCategory(CategoryVM category)
        {
            var cate = new Entities.Category()
            {
                Image = category.Image != null ? await _uploadService.SaveFile(category.Image) : "",
                Name = category.Name,
                Status = true,
            };

            await _context.Categories.AddAsync(cate);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(long id)
        {
            var cate = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(cate);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Category>> GetCategories()
        {
            var cates = await _context.Categories.Include(x => x.Events).ToListAsync();

            cates.ForEach(x => x.Image = _uploadService.GetFullPath(x.Image));

            return cates;
        }

        public async Task<Entities.Category> GetCategory(long id)
        {
            var cate = await _context.Categories.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == id);
            cate.Image = _uploadService.GetFullPath(cate.Image);
            return cate;
        }

        public async Task UpdateCategory(CategoryVM category)
        {
            var cate = await _context.Categories.FindAsync(category.Id);
            cate.Name = category.Name;
            cate.Status = category.Status;
            var img = cate.Image;
            if (category.Image != null)
                cate.Image = await _uploadService.SaveFile(category.Image);

            _context.Categories.Update(cate);
            await _context.SaveChangesAsync();

            if (category.Image != null)
                await _uploadService.DeleteFile(img);
        }
    }
}