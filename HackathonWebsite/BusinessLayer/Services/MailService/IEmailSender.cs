namespace HackathonWebsite.BusinessLayer.Services.MailService
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}
