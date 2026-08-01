using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

public class EmailServices : IEmailServices
{
    private readonly EmailSettings _emailSettings;
    public EmailServices(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            if (_emailSettings.From == null)
                throw new ArgumentNullException(nameof(_emailSettings.From));

            message.From.Add(new MailboxAddress(_emailSettings.Name, _emailSettings.From));
            message.To.Add(new MailboxAddress(body, toEmail)); // ← dùng tên thật
            message.Subject = subject;

            using (var sourceReader = System.IO.File.OpenText("Helpers/Template.html"))
            {
                bodyBuilder.HtmlBody = sourceReader.ReadToEnd();
                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{name}", body); // ← FIX
            }

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, int.Parse(_emailSettings.Port!), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.From, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }
}