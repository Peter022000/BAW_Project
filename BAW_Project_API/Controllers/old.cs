/*using AutoMapper;
using BAW_Project_API.Dtos;
using BAW_Project_API.Entities;
using BAW_Project_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BAW_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class asd : ControllerBase
    {
        private readonly  IUserService _userService;
        private readonly IMapper _mapper;

        public asd(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            List<User> users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUser(int id)
        {
            User users = await _userService.GetUserById(id);

            if (users == null) { 
                return NotFound("User not found");
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var ifExist = await _userService.GetUserByLogin(userDto.Name);

            if (ifExist.Count != 0)
            {
                return BadRequest("User exist");
            }

            await _userService.AddUser(user);

            return Ok(user);
        }
        
        [HttpPut("id")]
        public async Task<ActionResult> UpdateUser(int id,UserDto userDto)
        {
            var userDB = await _userService.GetUserById(id);

            var ifExist = await _userService.GetUserByLogin(userDto.Name);

            if (userDB is null)
            {
                return NotFound("User does not exist");
            } else if (ifExist.Count != 0)
            {
                return BadRequest("Login exist");
            }

            await _userService.UpdateUser(userDB,userDto);

            return Ok("User updated");
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            
            if (user is null)
            {
                return NotFound("User does not exist");
            }

            await _userService.DeleteUser(user);

            return Ok("User deleted");
        }
    }
}
*/