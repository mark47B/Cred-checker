using Microsoft.AspNetCore.Mvc;
using Credentials_checker.Data;
using Credentials_checker.Models;

namespace Credentials_checker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YourController : ControllerBase
    {
        private readonly DbContext _context;

        public YourController(DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CheckAccess([FromBody] User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Login) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid input.");
            }

            // Поиск пользователя в базе данных по логину и паролю
            bool access = _context.Users.Any(u => u.Login == user.Login && u.Password == user.Password);

            return Ok(new { access });
        }
    }
}
