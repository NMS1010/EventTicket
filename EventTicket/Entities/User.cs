namespace EventTicket.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }
		public string Password { get; set; }

		public bool Status { get; set; } = true;

		public long Role { get; set; }
	}
}