using Enterprise.Example.Data.Repositories;
using Enterprise.Example.Domain;
using Microsoft.Extensions.Logging;

namespace Enterprise.Example.Service
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository Repository { get; }
        public ILogger<NotificationService> Logger { get; }

        public NotificationService(INotificationRepository repository, ILogger<NotificationService> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public IList<Notification> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public async Task<Notification> GetByNameAsync(string name)
        {
            return await Repository.GetByNameAsync(name);
        }
    }
}
