using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SfCompulsory_cs.Data;
using SfCompulsory_cs.Models;
using SfCompulsory_cs.Services;

namespace SfCompulsory_cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔒 Protege todas as rotas com JWT
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly LogService _logService;

        public UsersController(ApplicationDbContext context, LogService logService)
        {
            _context = context;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _logService.AddLog("INFO", $"Usuário criado: {user.Username}", user.IdUser);

            return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
        {
            if (id != user.IdUser) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await _logService.AddLog("INFO", $"Usuário atualizado: {user.Username}", user.IdUser);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            await _logService.AddLog("WARN", $"Usuário deletado: {user.Username}", user.IdUser);

            return NoContent();
        }

    }
}
