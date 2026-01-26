using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/employment")]
public class EmploymentController(PortfolioContext dbContext) : ControllerBase
{
    private readonly PortfolioContext _dbContext = dbContext;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employment = await _dbContext.Employments.FirstOrDefaultAsync(x => x.Id == id);
        if (employment == null)
        {
            return NotFound();
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
    [HttpPost("add")]
    public IActionResult Add()
    {
        // TODO: Implement
        return Unauthorized();
    }
}