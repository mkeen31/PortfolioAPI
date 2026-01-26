using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/experience")]
public class ExperienceController(PortfolioContext dbContext) : ControllerBase
{
    private readonly PortfolioContext _dbContext = dbContext;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {

        var experience = await _dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id);
        if (experience == null)
        {
            return NotFound();
        }
        return Ok(experience);
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var experiences = await _dbContext.Experiences.ToListAsync();
        return Ok(experiences);
    }

    [Authorize]
    [HttpPost("[action]")]
    public IActionResult Add()
    {
        // TODO: Implement
        return Unauthorized();
    }
}