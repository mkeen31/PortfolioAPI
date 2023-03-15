using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly ILogger<ExperienceController> _logger;
    private readonly PortfolioContext _context;

    public ExperienceController(ILogger<ExperienceController> logger, PortfolioContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var experience = await _context.Experiences.FirstOrDefaultAsync(x => x.Id == id);
            if (experience == null)
            {
                return NotFound();
            }
            return new JsonResult(experience);
        }
        catch (Exception ex)
        {
            _logger.LogError(0, ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var experiences = await _context.Experiences.ToListAsync();
            return new JsonResult(experiences);
        }
        catch (Exception ex)
        {
            _logger.LogError(0, ex, ex.Message);
            return StatusCode(500);
        }
    }

    [HttpPost("[action]")]
    public IActionResult Add()
    {
        // TODO: Implement
        return Unauthorized();
    }
}