namespace HackathonWebsite.BusinessLayer.Services.MailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IMailService mailService;
        public EmailSender(IMailService mailService)
        {
            this.mailService = mailService;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            var mailData = new MailData(
                to: new List<string> { email },
                subject: subject,
                body: message,
                from: "TestMessagesService@yandex.ru",
                displayName: "Hackaton",
                bcc: new List<string> { "TestMessagesService@yandex.ru" },
                cc: new List<string> { "TestMessagesService@yandex.ru" }
            );

            var result = await mailService.SendAsync(mailData);

            return result;
        }
    }
}
