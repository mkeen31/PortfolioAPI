using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly ILogger<EducationController> _logger;
    private readonly PortfolioContext _context;

    public EducationController(ILogger<EducationController> logger, PortfolioContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try 
        {
            var education = await _context.Educations.FirstOrDefaultAsync(x => x.Id == id);
            if (education == null)
            {
                return NotFound();
            }
            return new JsonResult(education);
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
            var educations = await _context.Educations.ToListAsync();
            return new JsonResult(educations);

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