using CommandLine;
using Enterprise.Example.ConsoleRunner.Runner;
using Enterprise.Example.Core.Email;
using Enterprise.Example.Data;
using Enterprise.Example.Data.Repositories;
using Enterprise.Example.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Enterprise.Example.ConsoleRunner
{
    public class Program
    {
        // This is a comment
        private static IConfiguration Configuration { get; set; }
        private static Serilog.ILogger Logger { get; set; }

        static Program()
        {
            // Make sure all unexpected error is handled 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Initailise configuration
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .CreateLogger();
        }

        private static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json" ?? "PROD", optional: true, reloadOnChange: true);
        }

        public static void Main(string[] args)
        {
            Logger.Information("Application Started!");

            // Initialise the host builder and configure the DI Containers
            var host = CreateHost(args);

            // Parse the command line options and run the application
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(async opts => await RunJobRunner(opts, host))
                .WithNotParsed(errs => {
                    Logger.Error($"Valid CommandLine Parameters was not provided!");
                 });

            Logger.Information("Application Completed!");
        }

        private static async Task RunJobRunner(CommandLineOptions opts, IHost host)
        {
            var jobRunner = host.Services.GetRequiredService<IJobRunner>();
            await jobRunner.ExecuteAsync(opts);
        }

        private static IHost CreateHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    Configuration = context.Configuration;
                    var databaseServer = Configuration.GetValue<string>("DATABASE:ID");
                    var connectionString = Configuration.GetConnectionString(databaseServer);

                    // Add Database Context
                    services.AddDbContext<EnterpriseDbContext>(
                        options => options.UseSqlServer(connectionString)
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())
                    ));

                    // Add Repository Dependency Injection
                    services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
                    services.AddTransient<INotificationRepository, NotificationRepository>();

                    // Add Utility Objects
                    services.AddTransient<IEmailer, Emailer>();
                    services.AddTransient<IJobRunner, JobRunner>();

                    // Add Service Layer (the main entry point to the application)
                    services.AddTransient<INotificationService, NotificationService>();
                })
                .UseSerilog(Logger)
                .Build();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            if(exception != null)
            {
                if(exception.InnerException != null)
                {
                    Logger.Error($"INNER EXCEPTION                    :   {exception.InnerException.Message}");
                    Logger.Error($"STACK TRACE                        :   {exception.InnerException.StackTrace}");
                }

                Logger.Error($"APPLICATION EXCEPTION                  :   {exception.Message}");
                Logger.Error($"STACK TRACE                            :   {exception.StackTrace}");
            }

            Environment.Exit(-1);
        }
    }
}