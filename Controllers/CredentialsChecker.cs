using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AccessControlService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Login = "dsfsdf", Password = "sdfsdf" },
            new User { Login = "admin", Password = "admin" },
            // Добавьте других пользователей по необходимости
        };

        [HttpPost]
        public IActionResult CheckAccess([FromBody] JObject credentials)
        {
            if (credentials == null || !credentials.ContainsKey("Login") || !credentials.ContainsKey("Password"))
            {
                return BadRequest("Invalid input format.");
            }

            string login = credentials["Login"].ToString();
            string password = credentials["Password"].ToString();

            bool hasUser = users.Any(u => u.Login == login && u.Password == password);

            var response = new { access = hasUser };
            return Ok(response);
        }
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
