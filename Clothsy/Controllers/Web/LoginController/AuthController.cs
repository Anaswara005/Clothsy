using Clothsy.Data;
using Clothsy.DTOs.Web.Login;
using Clothsy.Services.AuthServices.Signup_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtpAuthApi.Services;

[ApiController]
[Route("api/web/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AuthController(
        ApplicationDbContext context,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _context = context;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await _context.WebUsers.FirstOrDefaultAsync(u =>
            u.Email == dto.Identifier ||
            u.PhoneNumber == dto.Identifier);

        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!_passwordService.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        if (!user.IsActive)
            return Unauthorized("Account disabled");

        if (user.Role != "ADMIN" && user.Role != "HUB")
            return Unauthorized("Website access denied");

        // ✅ match your existing token service
        var token = _tokenService.GenerateToken(user.Id, user.Email);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Role = user.Role
        });
    }
}
