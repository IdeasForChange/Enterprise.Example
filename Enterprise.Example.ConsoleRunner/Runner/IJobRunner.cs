namespace Enterprise.Example.ConsoleRunner.Runner
{
    public interface IJobRunner
    {
        Task ExecuteAsync(CommandLineOptions options);
    }
}
