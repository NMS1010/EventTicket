using System.ComponentModel.DataAnnotations;

namespace EventTicket.Models.Category
{
    public class CategoryVM
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}