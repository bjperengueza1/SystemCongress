using Application.Users.DTOs;
using Application.Users.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _userService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var userDto = await _userService.GetByIdAsync(id);
            
            return userDto == null ? NotFound() : Ok(userDto);
        }
        
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserInsertDto insertDto)
        {
            var userDto = await _userService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetUser), new { id = userDto.UserId}, null);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDto updateDto)
        {
            var userDto = await _userService.UpdateAsync(id, updateDto);
            
            return userDto == null ? NotFound() : Ok(userDto);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _userService.GetByEmail(loginDto.Email);
            
            if (user == null) return Unauthorized();
            
            var userDto = await _userService.Authenticate(loginDto);
            
            if (userDto == null) return Unauthorized();
            
            return Ok(userDto);
           
        }
        
    }
}
