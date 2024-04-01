namespace EmailAPIApplication.Services
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
