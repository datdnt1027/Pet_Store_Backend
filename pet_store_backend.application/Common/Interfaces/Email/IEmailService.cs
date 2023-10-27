using MimeKit;
using pet_store_backend.domain.Entities;

namespace pet_store_backend.application.Common.Interfaces.Email;

public interface IEmailService
{
    void SendEmail(Message message);
}