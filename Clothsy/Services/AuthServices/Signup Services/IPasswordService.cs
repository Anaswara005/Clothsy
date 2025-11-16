
namespace Clothsy.Services.AuthServices.Signup_Services


    {
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }

    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // Using BCrypt for password hashing
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            // Verify the password against the hash
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
