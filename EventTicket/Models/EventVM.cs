using EventTicket.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventTicket.Models
{
    public class EventVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public IFormFile Image { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [Required]
        public long TopicId { get; set; }

        [Required]
        public long PlaceId { get; set; }
    }
}