using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;




[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        try
        {
            // ���� ��� ����������� ������ "Client"
            var user = await _userService.RegisterAsync(userDto.Email, userDto.Password, "Client");
            return Ok(new { user.Email });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userDto)
    {
        var user = await _userService.AuthenticateAsync(userDto.Email, userDto.Password);
        if (user == null)
            return Unauthorized(new { error = "Invalid credentials" });

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }


    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(
            _configuration["Jwt:Secret"]
            ?? throw new ArgumentNullException("Jwt:Secret not found in configuration.")
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role) // ��������� ���� � �����
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpDelete("delete/{userId}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var success = await _userService.DeleteUserAsync(userId);
        if (!success)
            return NotFound(new { error = "User not found" });

        return NoContent();
    }

    [HttpGet("all")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }



    [HttpPost("add")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> Add([FromBody] UserDto userDto)
    {
        try
        {
            
            var user = await _userService.RegisterAsync(userDto.Email, userDto.Password, userDto.Role ?? "Client");
            return Ok(new { user.Email });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public record UserDto(string Email, string Password, string? Role = null);

