using EventTicket.Models;
using EventTicket.Repository.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Repository.Topic
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTopic(TopicVM topic)
        {
            var tp = new Entities.Topic()
            {
                Name = topic.Name,
                Status = true,
            };

            await _context.Topics.AddAsync(tp);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopic(long id)
        {
            var topic = await _context.Topics.FindAsync(id);

            _context.Topics.Remove(topic);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Topic>> GetTopics()
        {
            var topics = await _context.Topics.Include(x => x.Events).ToListAsync();

            return topics;
        }

        public async Task<Entities.Topic> GetTopic(long id)
        {
            var topic = await _context.Topics.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == id);

            return topic;
        }

        public async Task UpdateTopic(TopicVM topic)
        {
            var tp = await _context.Topics.FindAsync(topic.Id);
            tp.Name = topic.Name;
            tp.Status = topic.Status;

            _context.Topics.Update(tp);
            await _context.SaveChangesAsync();
        }
    }
}