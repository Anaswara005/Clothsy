
namespace Clothsy.Services.AuthServices.Signup_Services


    {
        public interface IOtpService
        {
            string GenerateOtp();
            Task<bool> SendOtpAsync(string contactType, string contactValue, string otp);
        }
    }
