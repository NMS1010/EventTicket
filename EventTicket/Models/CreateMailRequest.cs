namespace EventTicket.Models
{
	public class CreateMailRequest
	{
		public string Name { get; set; }
		public string Content { get; set; }

		public string Email { get; set; }
		public string Title { get; set; }
	}
}