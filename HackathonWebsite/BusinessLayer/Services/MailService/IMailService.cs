namespace HackathonWebsite.BusinessLayer.Services.MailService
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData);
    }
}
