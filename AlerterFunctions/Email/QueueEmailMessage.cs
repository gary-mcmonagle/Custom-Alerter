namespace AlerterFunctions.Email;

public record QueueEmailMessage
{
    public string Subject { get; init; }
    public string Body { get; init; }
}
