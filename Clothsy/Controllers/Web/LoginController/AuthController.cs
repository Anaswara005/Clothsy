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
        var identifier = dto.Identifier.Trim();

        // Normalize phone number
        var normalizedPhone = identifier
            .Replace(" ", "")
            .Replace("+", "");

        if (normalizedPhone.StartsWith("91") && normalizedPhone.Length > 10)
        {
            normalizedPhone = normalizedPhone.Substring(normalizedPhone.Length - 10);
        }

        var user = await _context.WebUsers.FirstOrDefaultAsync(u =>
            u.Email == identifier ||
            u.PhoneNumber.EndsWith(normalizedPhone));

        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!_passwordService.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        if (!user.IsActive)
            return Unauthorized("Account disabled");

        if (user.Role != "ADMIN" && user.Role != "HUB")
            return Unauthorized("Website access denied");

        int? hubId = null;
        string? district = null; // ✅ ADD DISTRICT VARIABLE

        if (user.Role == "HUB")
        {
            // ✅ FETCH BOTH HubId AND District
            var hub = await _context.Hubs
                .Where(h => h.HubCode == user.HubCode && h.IsActive)
                .Select(h => new { h.Id, h.District })
                .FirstOrDefaultAsync();

            if (hub == null)
                return Unauthorized("Hub not mapped or inactive");

            hubId = hub.Id;
            district = hub.District; // ✅ STORE DISTRICT
        }

        // ✅ PASS DISTRICT TO TOKEN GENERATION
        var token = _tokenService.GenerateWebToken(
            user.Id,
            user.Email,
            user.Role,
            hubId,
            district // ✅ NEW PARAMETER
        );

        return Ok(new LoginResponseDto
        {
            Token = token,
            Role = user.Role
        });
    }
}