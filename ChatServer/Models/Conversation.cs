using System;
using System.Collections.Generic;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<ConversationUser> Participants { get; set; } = new();
    public List<Message> Messages { get; set; } = new();
}

public class ConversationUser
{
    public int Id { get; set; }
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; }
    public string UserId { get; set; }
}
