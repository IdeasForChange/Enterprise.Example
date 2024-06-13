using CommandLine;

namespace Enterprise.Example.ConsoleRunner.Runner
{
    public sealed class CommandLineOptions
    {
        [Option('d', "BusinessDate", Required = true, HelpText = "Business date is a required parameter!")]
        public DateTime BusinessDate { get; set; }

        [Option('n', "Name", Required = true, HelpText = "Notification name is a required to identify the correct notofication to be sent!")]
        public string Name { get; set; }
    }
}
