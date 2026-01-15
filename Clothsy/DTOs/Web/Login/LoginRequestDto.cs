namespace Clothsy.DTOs.Web.Login
{
    public class LoginRequestDto
    {
        public string Identifier { get; set; } = null!; // email or phone
        public string Password { get; set; } = null!;
    }
}
