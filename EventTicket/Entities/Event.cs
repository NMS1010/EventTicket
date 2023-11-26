namespace EventTicket.Entities
{
	public class Event : BaseEntity
	{
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public string Organizer { get; set; }
		public Category Category { get; set; }
		public Topic Topic { get; set; }
		public Place Place { get; set; }
		public User User { get; set; }
		public ICollection<UserTicket> UserTickets { get; set; }
	}
}