namespace EventTicket.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}