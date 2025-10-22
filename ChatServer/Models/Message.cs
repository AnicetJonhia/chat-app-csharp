using System;
using System.Collections.Generic;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; }

    public string SenderId { get; set; }
    public ApplicationUser Sender { get; set; }

    public string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public List<FileAttachment> Attachments { get; set; } = new();
}
