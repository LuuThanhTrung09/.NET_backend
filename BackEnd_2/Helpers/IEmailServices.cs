public interface IEmailServices
{
    Task SendEmail(string toEmail, string subject, string body);
}