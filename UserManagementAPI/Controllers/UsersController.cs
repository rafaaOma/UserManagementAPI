using UserManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;//dependency injection of IUserService

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers() //retrive all users
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)//retrive specific user
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user) //add new user
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")] //update user by it's id
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                _userService.UpdateUser(user);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")] 
        public IActionResult DeleteUser(int id)//delete user by it's id
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}