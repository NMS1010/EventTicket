namespace EventTicket.Entities
{
	public class UserTicket : BaseEntity
	{
		public long UserId { get; set; }
		public long EventId { get; set; }
		public User User { get; set; }
		public Event Event { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}