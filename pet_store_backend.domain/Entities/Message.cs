using MimeKit;

namespace pet_store_backend.domain.Entities;

public class Message
{
    public List<MailboxAddress>? To { get; private set; }

    public string? Subject { get; private set; }

    public string? Content { get; private set; }

    public Message(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        Subject = subject;
        Content = content;
    }
}