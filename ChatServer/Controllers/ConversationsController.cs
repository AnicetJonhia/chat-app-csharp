using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ConversationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ConversationsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateConversationDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var conv = new Conversation { Title = dto.Title };
        conv.Participants.Add(new ConversationUser { UserId = userId });

        if (dto.ParticipantIds != null)
        {
            foreach (var pid in dto.ParticipantIds)
            {
                conv.Participants.Add(new ConversationUser { UserId = pid });
            }
        }

        _db.Conversations.Add(conv);
        await _db.SaveChangesAsync();
        return Ok(conv);
    }

    [HttpGet("my")]
    public async Task<IActionResult> MyConversations()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var convs = await _db.Conversations
            .Include(c => c.Participants)
            .Include(c => c.Messages)
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .ToListAsync();

        return Ok(convs);
    }
}

public record CreateConversationDto(string Title, string[] ParticipantIds);
