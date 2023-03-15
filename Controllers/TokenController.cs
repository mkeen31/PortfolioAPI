using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly PortfolioContext _context;

        public TokenController(ILogger<TokenController> logger, PortfolioContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Authenticate()
        {
            // TODO: Implement
            return Unauthorized();
        }
    }
}