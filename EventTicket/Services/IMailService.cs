using EventTicket.Models;

namespace EventTicket.Services
{
	public interface IMailService
	{
		void SendMail(CreateMailRequest request);
	}
}