using EventTicket.Models;
using EventTicket.Repository.DBContext;
using EventTicket.Services;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Repository.Event
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        private readonly IUploadService _uploadService;

        public EventRepository(AppDbContext context, IUploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public async Task AddEvent(EventVM ev)
        {
            var eventEntity = new Entities.Event()
            {
                Image = ev.Image != null ? await _uploadService.SaveFile(ev.Image) : "",
                Name = ev.Name,
                Status = ev.Status,
                Category = await _context.Categories.FindAsync(ev.CategoryId),
                Topic = await _context.Topics.FindAsync(ev.TopicId),
                Place = await _context.Places.FindAsync(ev.PlaceId),
                Description = ev.Description,
                EndDate = ev.EndDate,
                StartDate = ev.StartDate
            };

            await _context.Events.AddAsync(eventEntity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(long id)
        {
            var eventEntity = await _context.Events.FindAsync(id);

            _context.Events.Remove(eventEntity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Event>> GetEvents()
        {
            var eventEntitys = await _context.Events
                .Include(x => x.Category)
                .Include(x => x.Place)
                .Include(x => x.Topic).ToListAsync();

            eventEntitys.ForEach(x => x.Image = _uploadService.GetFullPath(x.Image));

            return eventEntitys;
        }

        public async Task<Entities.Event> GetEvent(long id)
        {
            var eventEntity = await _context.Events
                .Include(x => x.Category)
                .Include(x => x.Place)
                .Include(x => x.Topic).FirstOrDefaultAsync(x => x.Id == id);
            eventEntity.Image = _uploadService.GetFullPath(eventEntity.Image);
            return eventEntity;
        }

        public async Task UpdateEvent(EventVM ev)
        {
            var eventEntity = await _context.Events.FindAsync(ev.Id);

            eventEntity.Name = ev.Name;
            eventEntity.Status = ev.Status;
            eventEntity.Category = await _context.Categories.FindAsync(ev.CategoryId);
            eventEntity.Topic = await _context.Topics.FindAsync(ev.TopicId);
            eventEntity.Place = await _context.Places.FindAsync(ev.PlaceId);
            eventEntity.Description = ev.Description;
            eventEntity.EndDate = ev.EndDate;
            eventEntity.StartDate = ev.StartDate;

            var img = eventEntity.Image;
            if (ev.Image != null)
                eventEntity.Image = await _uploadService.SaveFile(ev.Image);

            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();

            if (ev.Image != null)
                await _uploadService.DeleteFile(img);
        }
    }
}