using EventTicket.Entities;
using EventTicket.Models;

namespace EventTicket.Repository.User
{
	public interface IUserRepository
	{
		Task<bool> Register(RegisterVM request);

		Task<Entities.User> Login(LoginVM request);

		Task<Entities.User> GetById(long id);

		Task<bool> Toggle(long id);

		Task<List<Entities.User>> GetAll();

		Task EditInfoUser(UserEditVM vm);

		Task EditAvatarUser(UserEditAvatar vm);

		Task RegisterEvent(RegisterEventVM vm);
	}
}