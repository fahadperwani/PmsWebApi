using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PmsWebApi.Data;
using PmsWebApi.DTOs;
using PmsWebApi.Mappers;
using PmsWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PmsWebApi.Services;

namespace PmsWebApi.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAuthService _authservice;

        public AppointmentController(ApplicationDbContext context, IConfiguration config, IAuthService authService)
        {
            _context = context;
            _config = config;
            _authservice = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");

            if (token == null) return Unauthorized();
            var userId = await _authservice.GetUserId(token);
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == userId || a.DoctorId == userId)
                .ToListAsync();

            return Ok(appointments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");

            if (token == null) return Unauthorized();
            var userId = await _authservice.GetUserId(token);

            var appointment = dto.toAppointment();
            appointment.Time = DateTime.Now;

            if (userId != appointment.PatientId) return BadRequest();

            var result = await _context.Appointments.AddAsync(appointment);

            await _context.SaveChangesAsync();

            if (result != null) return Ok("Done");
            
            return BadRequest();
        }
    }
}
