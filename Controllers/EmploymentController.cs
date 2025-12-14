using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmploymentController(ILogger<EmploymentController> logger, PortfolioContext dbContext) : ControllerBase
{
    private readonly ILogger<EmploymentController> _logger = logger;
    private readonly PortfolioContext _dbContext = dbContext;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employment = await _dbContext.Employments.FirstOrDefaultAsync(x => x.Id == id);
        if (employment == null)
        {
            return Problem("Record not found.", statusCode: StatusCodes.Status404NotFound);
        }
        return Ok(employment);
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employments = await _dbContext.Employments.ToListAsync();
        return Ok(employments);
    }

    [Authorize]
    [HttpPost("[action]")]
    public IActionResult Add()
    {
        // TODO: Implement
        return Unauthorized();
    }
}