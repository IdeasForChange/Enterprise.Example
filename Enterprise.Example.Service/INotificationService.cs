using Enterprise.Example.Domain;

namespace Enterprise.Example.Service
{
    public interface INotificationService
    {
        IList<Notification> GetAll();

        Task<Notification> GetByNameAsync(string name);
    }
}
