using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.IO;
using System;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _db;

    public FilesController(IWebHostEnvironment env, ApplicationDbContext db) { _env = env; _db = db; }

    [HttpPost("upload/{messageId}")]
    public async Task<IActionResult> Upload(Guid messageId, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file");

        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        Directory.CreateDirectory(uploads);
        var stored = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine(uploads, stored);

        using (var stream = System.IO.File.Create(path))
        {
            await file.CopyToAsync(stream);
        }

        var fa = new FileAttachment
        {
            MessageId = messageId,
            FileName = file.FileName,
            StoredFileName = stored,
            Size = file.Length,
            ContentType = file.ContentType
        };
        _db.FileAttachments.Add(fa);
        await _db.SaveChangesAsync();

        return Ok(new { downloadUrl = $"/api/files/download/{fa.Id}" });
    }

    [AllowAnonymous]
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var fa = await _db.FileAttachments.FindAsync(id);
        if (fa == null) return NotFound();
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        var path = Path.Combine(uploads, fa.StoredFileName);
        if (!System.IO.File.Exists(path)) return NotFound();
        var bytes = await System.IO.File.ReadAllBytesAsync(path);
        return File(bytes, fa.ContentType, fa.FileName);
    }
}
