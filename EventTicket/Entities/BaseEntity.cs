using System.ComponentModel.DataAnnotations;

namespace EventTicket.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}