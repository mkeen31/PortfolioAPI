using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmploymentController : ControllerBase
{
    private readonly ILogger<EmploymentController> _logger;
    private readonly PortfolioContext _context;

    public EmploymentController(ILogger<EmploymentController> logger, PortfolioContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var employment = await _context.Employments.FirstOrDefaultAsync(x => x.Id == id);
            if (employment == null)
            {
                return NotFound();
            }
            return new JsonResult(employment);
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
            var employments = await _context.Employments.ToListAsync();
            return new JsonResult(employments);
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