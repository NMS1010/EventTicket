using EventTicket.Entities;
using EventTicket.Models;
using EventTicket.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace EventTicket.Repository.Place
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext _context;

        public PlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPlace(PlaceVM pl)
        {
            var place = new Entities.Place()
            {
                Address = pl.Address,
                CreatedBy = pl.CreatedBy,
                Description = pl.Description,
                Lat = pl.Lat,
                Long = pl.Long,
                Phone = pl.Phone,
                TimeActive = pl.TimeActive,
                UpdatedAt = DateTime.Now,
                Name = pl.Name,
                Status = true,
                PlaceId = pl.PlaceId
            };

            await _context.Places.AddAsync(place);

            await _context.SaveChangesAsync();
        }

        public async Task DeletePlace(long id)
        {
            var place = await _context.Places.FindAsync(id);

            _context.Places.Remove(place);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Place>> GetPlaces()
        {
            var places = await _context.Places.Include(x => x.Events).ToListAsync();

            return places;
        }

        public async Task<Entities.Place> GetPlace(long id)
        {
            var place = await _context.Places.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == id);

            return place;
        }

        public async Task UpdatePlace(PlaceVM pl)
        {
            var place = await _context.Places.FindAsync(pl.Id);

            place.Address = pl.Address;
            place.CreatedBy = pl.CreatedBy;
            place.Description = pl.Description;
            place.Lat = pl.Lat;
            place.Long = pl.Long;
            place.Phone = pl.Phone;
            place.TimeActive = pl.TimeActive;
            place.UpdatedAt = DateTime.Now;
            place.Name = pl.Name;
            place.Status = pl.Status;
            place.PlaceId = pl.PlaceId;

            _context.Places.Update(place);
            await _context.SaveChangesAsync();
        }
    }
}