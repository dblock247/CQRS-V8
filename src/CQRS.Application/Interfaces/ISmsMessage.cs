namespace CQRS.Application.Interfaces;

public interface ISmsMessage
{
    public string To { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
}
