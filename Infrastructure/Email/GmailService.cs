using System.Net;
using System.Net.Mail;
using Application.Common;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email;

public class GmailService : IEmailService
{
    private readonly GmailOptions _gmailOptions;
    
    public GmailService(IOptions<GmailOptions> gmailOptions)
    {
        _gmailOptions = gmailOptions.Value;
    }
    
    public async Task<bool> SendStatusChangeNotificationAsync(Exposure exposure)
    {
        try
        {
            //recipient es el email del primer autor de la exposicion ICollection<Author> Authors 
            var recipient = exposure.ExposureAuthor.FirstOrDefault()?.Author.PersonalMail;
            Console.WriteLine("Recipient: " + recipient);
            
            /*if (string.IsNullOrWhiteSpace(recipient))
            {
                Console.WriteLine("No email found for recipient");
                return false;
            }
            
            var subject = $"Status Changed: {exposure.Name}";
            var body = $"The status of your exposure titled '{exposure.Name}' has been changed to '{exposure.StatusExposure}'.";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_gmailOptions.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipient);
            
            using var smtpClient = new SmtpClient(_gmailOptions.Host, _gmailOptions.Port);
            smtpClient.Credentials = new NetworkCredential(_gmailOptions.Email, _gmailOptions.Password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);*/
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_gmailOptions.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            
            using var smtpClient = new SmtpClient(_gmailOptions.Host, _gmailOptions.Port);
            smtpClient.Credentials = new NetworkCredential(_gmailOptions.Email, _gmailOptions.Password);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return false;
        }
    }
    

    
}