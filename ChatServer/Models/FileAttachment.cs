using System;

public class FileAttachment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MessageId { get; set; }
    public Message Message { get; set; }

    public string FileName { get; set; }
    public string StoredFileName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
