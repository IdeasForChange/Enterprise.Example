using Enterprise.Example.Domain;
using Enterprise.Example.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Enterprise.Example.ConsoleRunner.Runner
{
    internal class JobRunner : IJobRunner
    {
        public IConfiguration Configuration { get; }
        public INotificationService NotificationService { get; }
        public ILogger<JobRunner> Logger { get; }

        public JobRunner(IConfiguration configuration, INotificationService notificationService, ILogger<JobRunner> logger)
        {
            Configuration = configuration;
            NotificationService = notificationService;
            Logger = logger;
        }

        public async Task ExecuteAsync(CommandLineOptions options)
        {
            // Show All Notofications
            var notifications = NotificationService.GetAll();
            notifications.ToList().ForEach(n => PrintData(n));

            // Show matching Notification
            var notification = await NotificationService.GetByNameAsync(options.Name);
            PrintData(notification);  
        }

        private void PrintData(Notification notification)
        {
            if (notification != null)
            {
                Logger.LogInformation($"FOUND DATA: Id:{notification.Id}, Name:{notification.Name}");
            }
            else
            {
                Logger.LogError($"NOT DATA FOUND:");
            }
        }
    }
}
