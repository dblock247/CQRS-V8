using CQRS.Application.Interfaces;

namespace CQRS.Application.Models;

public class TwilioMessage : ISmsMessage
{
    public string To { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
}
