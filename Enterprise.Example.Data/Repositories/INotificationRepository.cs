using Enterprise.Example.Domain;

namespace Enterprise.Example.Data.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification GetByName(string name);

        Task<Notification> GetByNameAsync(string name);
    }
}
