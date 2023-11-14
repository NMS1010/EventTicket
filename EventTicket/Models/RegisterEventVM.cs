using System.ComponentModel.DataAnnotations;

namespace EventTicket.Models
{
	public class RegisterEventVM
	{
		[Required]
		public long EventId { get; set; }

		[Required]
		public long UserId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Phone { get; set; }

		[Required]
		public string Address { get; set; }
	}
}