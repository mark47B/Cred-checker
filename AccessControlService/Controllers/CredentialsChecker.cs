using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AccessControlService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly List<User> users;

        public AccessController()
        {
            users = LoadUsersFromJson();
        }

        private List<User> LoadUsersFromJson()
        {
            var jsonPath = Path.Combine("Data", "users.json");
            if (!System.IO.File.Exists(jsonPath))
            {
                // If the JSON file doesn't exist, return an empty list of users.
                return new List<User>();
            }

            var json = System.IO.File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        [HttpPost]
        public IActionResult CheckAccess([FromBody] User requestBody)
        {   
            if (requestBody == null)
            {
                return BadRequest("Invalid input format. The 'credentials' field is required.");
            }

            string login = requestBody.Login;
            string password = requestBody.Password;

            bool hasUser = users.Any(u => u.Login == login && u.Password == password);

            var response = new { access = hasUser };
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            
            return Ok(response);
        }
        [HttpOptions]
        public IActionResult Options()
        {   
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            
            return Ok();
        }
    }

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
