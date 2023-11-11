namespace EventTicket.Entities
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Phone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string TimeActive { get; set; }
        public string Description { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string PlaceId { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}