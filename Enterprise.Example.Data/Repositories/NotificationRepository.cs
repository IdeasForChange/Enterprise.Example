using Enterprise.Example.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Example.Data.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(EnterpriseDbContext context) 
            : base(context)
        {
        }

        public Notification GetByName(string name)
        {
            var result = Context.Notifications.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return result;
        }

        public async Task<Notification> GetByNameAsync(string name)
        {
            var result = await GetAll().FirstOrDefaultAsync(x => x.Name == name);
            return result;
        }

    }
}
