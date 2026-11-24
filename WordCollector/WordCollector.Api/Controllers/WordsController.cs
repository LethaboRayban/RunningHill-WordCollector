using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordCollector.Api.Data;
using WordCollector.Shared;

namespace WordCollector.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly AppDbContext _context;

    public WordsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WordEntry>>> GetWords([FromQuery] WordType? type)
    {
        var query = _context.Words.AsQueryable();

        if (type.HasValue)
        {
            query = query.Where(x => x.Type == type.Value);
        }

        return Ok(await query.OrderByDescending(w => w.Id).ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<WordEntry>> AddWord(WordEntry item)
    {
        item.Word = item.Word.Trim();
        _context.Words.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWords), new { id = item.Id }, item);
    }

   
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWord(int id)
    {
        var word = await _context.Words.FindAsync(id);
        if (word is null) return NotFound();

        _context.Words.Remove(word);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
