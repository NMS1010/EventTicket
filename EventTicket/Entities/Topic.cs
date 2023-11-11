namespace EventTicket.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}