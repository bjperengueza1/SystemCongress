using Domain.Entities;

namespace Application.Common;

public interface IEmailService
{
    Task<bool> SendStatusChangeNotificationAsync(Exposure exposure);
    
    Task<bool> SendEmailAsync(string email, string subject, string body);

    Task<bool> SendEmailAsync(string[] emails, string subject, string body);

}