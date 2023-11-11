using EventTicket.Entities;
using EventTicket.Models.Category;

namespace EventTicket.Repository.Category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Entities.Category>> GetCategories();

        Task<Entities.Category> GetCategory(long id);

        Task AddCategory(CategoryVM category);

        Task UpdateCategory(CategoryVM category);

        Task DeleteCategory(long id);
    }
}