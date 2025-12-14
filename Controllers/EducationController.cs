using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController(PortfolioContext dbContext) : ControllerBase
{
    private readonly PortfolioContext _dbContext = dbContext;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {

        var education = await _dbContext.Educations.FirstOrDefaultAsync(x => x.Id == id);
        if (education == null)
        {
            return Problem("Record not found.", statusCode: StatusCodes.Status404NotFound);
        }
        return Ok(education);

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var educations = await _dbContext.Educations.ToListAsync();
        return Ok(educations);
    }

    [Authorize]
    [HttpPost("[action]")]
    public IActionResult Add()
    {
        // TODO: Implement
        return Unauthorized();
    }
}