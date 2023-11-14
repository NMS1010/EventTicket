using System.ComponentModel.DataAnnotations;

namespace EventTicket.Models
{
	public class UserEditAvatar
	{
		[Required]
		public long Id { get; set; }

		[Required]
		public IFormFile Avatar { get; set; }
	}
}