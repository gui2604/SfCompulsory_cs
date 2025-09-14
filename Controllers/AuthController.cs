using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SfCompulsory_cs.Data;
using SfCompulsory_cs.Dtos;
using SfCompulsory_cs.Models;
using SfCompulsory_cs.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SfCompulsory_cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase 
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly LogService _logService;

        public AuthController(IConfiguration config, ApplicationDbContext context, LogService logService)
        {
            _config = config;
            _context = context;
            _logService = logService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                await _logService.AddLog("WARN", $"Tentativa de login com usuário inexistente: {loginDto.Username}");
                return Unauthorized(new { message = "Usuário não encontrado" });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                await _logService.AddLog("WARN", $"Senha inválida para usuário: {loginDto.Username}");
                return Unauthorized(new { message = "Senha inválida" });
            }

            var token = GenerateJwtToken(user);

            await _logService.AddLog("INFO", $"Login bem-sucedido para usuário: {user.Username}", user.IdUser);

            return Ok(new { token });
        }


        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("userId", user.IdUser.ToString()),
                new Claim("email", user.Email ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
