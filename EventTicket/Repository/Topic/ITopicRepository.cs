using EventTicket.Models;

namespace EventTicket.Repository.Topic
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Entities.Topic>> GetTopics();

        Task<Entities.Topic> GetTopic(long id);

        Task AddTopic(TopicVM topic);

        Task UpdateTopic(TopicVM topic);

        Task DeleteTopic(long id);
    }
}