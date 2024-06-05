using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PmsWebApi.Data;

namespace PmsWebApi.Controllers
{
    [Route("/api/doctor")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var doctors =await _context.Users.Where(user => user.isDoctor && EF.Functions.Like(user.Name, "%" + name + "%")).ToListAsync();

            if(doctors == null || doctors.Count == 0) { return NotFound(); }
            return Ok(doctors);
        }
    }
}
