using Domain.Entities;

namespace Application.Common;

public interface IEmailService
{
    Task<bool> SendStatusChangeNotificationAsync(Exposure exposure);

}