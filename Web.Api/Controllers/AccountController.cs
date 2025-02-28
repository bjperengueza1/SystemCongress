using Application.Token;
using Application.Users.DTOs;
using Application.Users.Interfaces;
using Domain.Filter;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserFilter filter)
        {
            if (filter.pageNumber <= 0 || filter.pageSize <= 0)
            {
                return BadRequest("El número de página y el tamaño deben ser mayores a 0.");
            }
            
            var users = await _userService.GetPagedAsync(filter);
            
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var userDto = await _userService.GetByIdAsync(id);
            
            return userDto == null ? NotFound() : Ok(userDto);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddUser([FromBody] UserInsertDto insertDto)
        {
            var userDto = await _userService.CreateAsync(insertDto);
            
            return CreatedAtAction(nameof(GetUser), new { id = userDto.UserId}, null);
        }
        
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDto updateDto)
        {
            var userDto = await _userService.UpdateAsync(id, updateDto);
            
            return userDto == null ? NotFound() : Ok(userDto);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserLoggedDto>> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _userService.GetByEmail(loginDto.Email);
            
            if (user == null) return Unauthorized();
            
            var userLoggedDto = await _userService.Authenticate(loginDto);
            
            if (userLoggedDto == null) return Unauthorized();
            
            return Ok(userLoggedDto);
           
        }
        
        //change password by admin
        [HttpPut("change-password-by-admin/{id:int}")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] UserChangePasswordByAdminDto changePasswordByAdminDto)
        {
            var userDto = await _userService.ChangePasswordByAdminAsync(id, changePasswordByAdminDto);
            
            return userDto == false ? NotFound() : Ok();
        }
        
        [HttpPut("change-password/{id:int}")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] UserChangePasswordDto changePasswordDto)
        {
            var userDto = await _userService.ChangePasswordAsync(id, changePasswordDto);
            
            return userDto == false ? NotFound() : Ok();
        }
    }
}
