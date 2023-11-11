using EventTicket.Models;

namespace EventTicket.Repository.Place
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Entities.Place>> GetPlaces();

        Task<Entities.Place> GetPlace(long id);

        Task AddPlace(PlaceVM pl);

        Task UpdatePlace(PlaceVM pl);

        Task DeletePlace(long id);
    }
}