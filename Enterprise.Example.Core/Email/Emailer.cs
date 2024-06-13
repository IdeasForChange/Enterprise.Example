using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace Enterprise.Example.Core.Email
{
    public class Emailer : IEmailer, IDisposable
    {
        private readonly SmtpClient _smtpClient;

        public IConfiguration Configuration { get; }
        public ILogger<Emailer> Logger { get; }

        public string From => Configuration.GetValue<string>("SMTP:FROM");
        public string SmtpServer => Configuration.GetValue<string>("SMTP:SERVER");
        public int SmtpPort => Configuration.GetValue<int>("SMTP:PORT");

        public Emailer(IConfiguration configuration, ILogger<Emailer> logger)
        {
            Configuration = configuration;
            Logger = logger;

            _smtpClient = new SmtpClient(SmtpServer, SmtpPort);
        }

        public void SendMail(string fromAddress, string toAddresses, string subject, string body)
        {
            SendMail(fromAddress, toAddresses, subject, body, new List<Attachment>());
        }

        public void SendMail(string fromAddress, string toAddresses, string subject, string body, IEnumerable<Attachment> attachments)
        {
            var recipients = toAddresses.Split(',', ';').ToList();

            var message = new MailMessage()
            {
                From = new MailAddress(From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (var attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }

            Logger.LogInformation($"Sending email via: SMTP SERVER: {SmtpServer}, PORT: {SmtpPort}, RECIPIENTS: {toAddresses}");
            
            // Send the message
            _smtpClient.Send(message);

            // Dispose mail attachments (if it is in memory attachments)
            message.Attachments.ToList().ForEach(x => x.ContentStream.Dispose());
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
        }    
    }
}
