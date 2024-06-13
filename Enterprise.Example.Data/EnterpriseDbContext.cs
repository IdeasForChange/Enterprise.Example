using Enterprise.Example.Data.Mappings;
using Enterprise.Example.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Enterprise.Example.Data
{
    public class EnterpriseDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public ILogger<EnterpriseDbContext> Logger { get; }
        public DbSet<Notification> Notifications { get; set; }

        public EnterpriseDbContext(DbContextOptions<EnterpriseDbContext> options, IConfiguration configuration, ILogger<EnterpriseDbContext> logger) 
            : base(options)
        {
            Configuration = configuration;
            Logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                var databaseServer = Configuration.GetValue<string>("DATABASE:ID");
                var connectionString = Configuration.GetConnectionString(databaseServer);

                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseLoggerFactory(LoggerFactory.Create(o => o.AddConsole()));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        }
    }
}
