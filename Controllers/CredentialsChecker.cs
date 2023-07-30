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
        public IActionResult CheckAccess([FromBody] JObject requestBody)
        {
            if (requestBody == null)
            {
                return BadRequest("Invalid input format. The 'credentials' field is required.");
            }

            if (requestBody == null || !requestBody.HasValues || !requestBody.ContainsKey("Login") || !requestBody.ContainsKey("Password"))
            {
                return BadRequest("Invalid input format. The 'Login' and 'Password' fields are required in 'credentials'.");
            }

            Console.WriteLine(requestBody);
            Console.WriteLine(requestBody["Login"].ToString());

            string login = requestBody["Login"].ToString();
            string password = requestBody["Password"].ToString();

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
