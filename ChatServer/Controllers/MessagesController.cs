using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using System.Linq;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<ChatHub> _hub;

    public MessagesController(ApplicationDbContext db, IHubContext<ChatHub> hub)
    {
        _db = db; _hub = hub;
    }

    [HttpPost("{conversationId}")]
    public async Task<IActionResult> Send(Guid conversationId, [FromBody] SendMessageDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var msg = new Message { ConversationId = conversationId, Content = dto.Content, SenderId = userId, SentAt = DateTime.UtcNow };
        _db.Messages.Add(msg);
        await _db.SaveChangesAsync();

        await _hub.Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", conversationId, userId, dto.Content, msg.SentAt);

        return Ok(msg);
    }

    [HttpGet("history/{conversationId}")]
    public async Task<IActionResult> History(Guid conversationId, int page = 1, int pageSize = 50)
    {
        var q = _db.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderByDescending(m => m.SentAt)
            .Skip((page-1)*pageSize)
            .Take(pageSize)
            .Include(m => m.Attachments);

        var list = await q.ToListAsync();
        return Ok(list);
    }
}

public record SendMessageDto(string Content);
