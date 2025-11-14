
namespace Clothsy.Services.AuthServices.Signup_Services

{
    public class OtpService : IOtpService
    {
        private readonly ILogger<OtpService> _logger;

        public OtpService(ILogger<OtpService> logger)
        {
            _logger = logger;
        }

        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task<bool> SendOtpAsync(string contactType, string contactValue, string otp)
        {
            // Simulate sending OTP
            await Task.Delay(100);

            if (contactType == "Phone")
            {
                _logger.LogInformation($"Sending OTP {otp} to phone: {contactValue}");
                // TODO: Integrate with SMS provider (Twilio, AWS SNS, etc.)
                // Example: await _smsService.SendAsync(contactValue, $"Your OTP is: {otp}");
            }
            else if (contactType == "Email")
            {
                _logger.LogInformation($"Sending OTP {otp} to email: {contactValue}");
                // TODO: Integrate with Email provider (SendGrid, AWS SES, etc.)
                // Example: await _emailService.SendAsync(contactValue, "OTP Verification", $"Your OTP is: {otp}");
            }

            return true;
        }
    }
}
