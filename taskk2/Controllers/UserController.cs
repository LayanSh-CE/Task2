using Microsoft.AspNetCore.Mvc;
using taskk2;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private static List<User> users = new List<User>
        {
            new User
            {
                ID=25,
                DateOfBirth = new DateTime(2012, 12, 1),
                FirstName = "Ahmad",
                LastName = "Shoukri",
                Email = "ahmadsamir@gmail.com"

            },
            new User
            {
                ID=33,
                DateOfBirth = new DateTime(2002, 8, 2),
                FirstName = "Layan",
                LastName = "Shoukri",
                Email = "layansamir@gmail.com"

            },
            new User
            {
                ID=18,
                DateOfBirth = new DateTime(2003, 7, 3),
                FirstName = "Ayyan",
                LastName = "Shoukri",
                Email = "ayyansamir@gmail.com"
            }
        };

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllData()
        {
            if (users == null || !users.Any())
            {
               return NotFound("There are no users in the list");
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpPost("AddUser")]
        public IActionResult PostDataUser([FromBody] User obj)
        {
            if (obj == null)
            {
                return BadRequest("Bad request");
            }

            else
            {
                for (int i = 0; i < users.Count; i++) {
                    if (users[i].ID == obj.ID) {
                        return Conflict("The ID you entered is already associated with an existing user\nChange the ID");
                    }
                }
                 users.Add(obj);
                 return Ok("User added successfully");
            }
        }

        [HttpDelete("DeleteUser")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(f => f.ID == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            users.Remove(user);
            return Ok("User deleted successfully");
        }

        [HttpGet("GetUserByID")]
        public IActionResult GetResult(int id)
        {
            var user = users.FirstOrDefault(f => f.ID == id);

            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok (user);
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(f => f.ID == id);

            if (user == null)
            {
                return NotFound("Data not found");
            }
            else
            {
                if (user.ID == updatedUser.ID)
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.DateOfBirth = updatedUser.DateOfBirth;
                    user.Email = updatedUser.Email;
                    return Ok("User updated successfully");
                }
                else
                {
                    return Ok("Dont change the ID");
                }
            }
        }
    }
}