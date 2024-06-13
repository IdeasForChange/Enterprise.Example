namespace Enterprise.Example.Core.Email
{
    public interface IEmailer
    {
        /// <summary>
        /// Allows the user to send an email by providing the following parameters
        /// </summary>
        /// <param name="fromAddress">From Address (Only one allowed)</param>
        /// <param name="toAddresses">A comma separated value of addresses where the mail will be sent to.</param>
        /// <param name="subject">Subject of the email!</param>
        /// <param name="body">Body of the email.</param>
        void SendMail(string fromAddress, string toAddresses, string subject, string body);
    }
}
