
using Microsoft.EntityFrameworkCore;
using Clothsy.Data;
using Clothsy.DTOs.SignupDTOs;
using Clothsy.Models.SignupModels;
using System.Text.RegularExpressions;
using OtpAuthApi.Services;


namespace Clothsy.Services.AuthServices.Signup_Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOtpService _otpService;

        public UserService(ApplicationDbContext context, IOtpService otpService)
        {
            _context = context;
            _otpService = otpService;
        }

        public async Task<ApiResponse<object>> SignupAsync(SignupRequest request)
        {
            // Validate full name
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Full name is required"
                };
            }

            // Validate contact type
            if (request.ContactType != "Phone" && request.ContactType != "Email")
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Contact type must be 'Phone' or 'Email'"
                };
            }

            string contactValue = string.Empty;

            // Validate based on contact type
            if (request.ContactType == "Phone")
            {
                if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Phone number is required when contact type is Phone"
                    };
                }

                // Validate phone format (10 digits)
                if (!Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Phone number must be exactly 10 digits"
                    };
                }

                contactValue = request.PhoneNumber;

                // Check if phone already exists in Users table
                var existingUserByPhone = await _context.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

                if (existingUserByPhone != null)
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User already registered with this phone number"
                    };
                }

                // Check if phone already exists in pending OtpLogs (signup in progress)
                var pendingOtpByPhone = await _context.OtpLogs
                    .Where(o => o.PhoneNumber == request.PhoneNumber && !o.IsUsed)
                    .OrderByDescending(o => o.CreatedAt)
                    .FirstOrDefaultAsync();

                if (pendingOtpByPhone != null)
                {
                    // If OTP is still valid, inform user
                    if (DateTime.Now <= pendingOtpByPhone.ExpiryTime)
                    {
                        return new ApiResponse<object>
                        {
                            Success = false,
                            Message = "A signup is already in progress with this phone number. Please verify the OTP or wait for it to expire."
                        };
                    }
                }
            }
            else // Email
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Email is required when contact type is Email"
                    };
                }

                // Validate email format
                if (!request.Email.Contains("@") || !request.Email.Contains("."))
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid email format"
                    };
                }

                contactValue = request.Email;

                // Check if email already exists in Users table
                var existingUserByEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (existingUserByEmail != null)
                {
                    return new ApiResponse<object>
                    {
                        Success = false,
                        Message = "User already registered with this email"
                    };
                }

                // Check if email already exists in pending OtpLogs (signup in progress)
                var pendingOtpByEmail = await _context.OtpLogs
                    .Where(o => o.Email == request.Email && !o.IsUsed)
                    .OrderByDescending(o => o.CreatedAt)
                    .FirstOrDefaultAsync();

                if (pendingOtpByEmail != null)
                {
                    // If OTP is still valid, inform user
                    if (DateTime.Now <= pendingOtpByEmail.ExpiryTime)
                    {
                        return new ApiResponse<object>
                        {
                            Success = false,
                            Message = "A signup is already in progress with this email. Please verify the OTP or wait for it to expire."
                        };
                    }
                }
            }

            // Generate OTP
            string otpCode = _otpService.GenerateOtp();

            // Create OTP log entry
            var otpLog = new OtpLog
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                ContactType = request.ContactType,
                ContactValue = contactValue,
                OtpCode = otpCode,
                ExpiryTime = DateTime.Now.AddMinutes(5),
                ResendCount = 0,
                IsUsed = false,
                CreatedAt = DateTime.Now
            };

            _context.OtpLogs.Add(otpLog);
            await _context.SaveChangesAsync();

            // Send OTP
            await _otpService.SendOtpAsync(request.ContactType, contactValue, otpCode);

            return new ApiResponse<object>
            {
                Success = true,
                Message = "OTP sent successfully",
                Data = new { contactValue = contactValue }
            };
        }

        public async Task<ApiResponse<object>> VerifyOtpAsync(VerifyOtpRequest request)
        {
            // Find the latest OTP log for this contact
            var otpLog = await _context.OtpLogs
                .Where(o => o.ContactType == request.ContactType &&
                           o.ContactValue == request.ContactValue &&
                           !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            if (otpLog == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "No signup request found"
                };
            }

            // Check if OTP is expired
            if (DateTime.Now > otpLog.ExpiryTime)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "OTP expired, please request a new one"
                };
            }

            // Verify OTP
            if (otpLog.OtpCode != request.OtpCode)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid OTP"
                };
            }

            // Mark OTP as used
            otpLog.IsUsed = true;
            _context.OtpLogs.Update(otpLog);

            // Create user
            var user = new User
            {
                FullName = otpLog.FullName,
                PhoneNumber = otpLog.PhoneNumber,
                Email = otpLog.Email,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Signup successful",
                Data = new { redirectTo = "/home" }
            };
        }

        public async Task<ApiResponse<object>> ResendOtpAsync(ResendOtpRequest request)
        {
            // Find existing OTP log
            var otpLog = await _context.OtpLogs
                .Where(o => o.ContactType == request.ContactType &&
                           o.ContactValue == request.ContactValue &&
                           !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();

            if (otpLog == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Signup not initiated"
                };
            }

            // Check resend limit
            if (otpLog.ResendCount >= 5)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Maximum OTP resend limit reached"
                };
            }

            // Generate new OTP
            string newOtpCode = _otpService.GenerateOtp();

            // Update OTP log
            otpLog.OtpCode = newOtpCode;
            otpLog.ExpiryTime = DateTime.Now.AddMinutes(5);
            otpLog.ResendCount++;

            _context.OtpLogs.Update(otpLog);
            await _context.SaveChangesAsync();

            // Send new OTP
            await _otpService.SendOtpAsync(request.ContactType, request.ContactValue, newOtpCode);

            return new ApiResponse<object>
            {
                Success = true,
                Message = "OTP resent successfully"
            };
        }
    }
}