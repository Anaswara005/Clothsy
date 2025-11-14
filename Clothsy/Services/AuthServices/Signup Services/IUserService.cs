

using Clothsy.DTOs.SignupDTOs;

namespace OtpAuthApi.Services
{
    public interface IUserService
    {
        Task<ApiResponse<object>> SignupAsync(SignupRequest request);
        Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpRequest request);
        Task<ApiResponse<object>> ResendOtpAsync(ResendOtpRequest request);
    }
}
