using EventTicket.Models;

namespace EventTicket.Repository.Event
{
    public interface IEventRepository
    {
        Task<IEnumerable<Entities.Event>> GetEvents();

        Task<Entities.Event> GetEvent(long id);

        Task AddEvent(EventVM ev);

        Task UpdateEvent(EventVM ev);

        Task DeleteEvent(long id);
    }
}