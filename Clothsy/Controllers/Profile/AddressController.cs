using Clothsy.Data;
using Clothsy.DTOs.Profile;
using Clothsy.Models.Profile;
using Clothsy.Models.SignupModels;
using ClothsyAPI.DTOs;
using ClothsyAPI.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClothsyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        
        // GET: api/address
        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetAddresses()
        {
            try
            {
                var userId = GetUserId();
                var addresses = await _context.Addresses
                    .Where(a => a.UserId == userId && !a.IsDeleted)  // ← ADD !a.IsDeleted
                    .Select(a => new
                    {
                        a.Id,
                        a.FullName,
                        a.PhoneNumber,
                        a.PinCode,
                        a.District,
                        a.HouseAddress,
                        a.RoadName,
                        a.CreatedAt
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<object>(
                    true,
                    "Addresses retrieved successfully",
                    addresses
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        // POST: api/address
        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> AddAddress([FromBody] AddressRequest request)
        {
            try
            {
                var userId = GetUserId(); // ✅ FIX
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return Unauthorized(new ApiResponse<object>(false, "User not found", null));
                }

                var address = new Address
                {
                    UserId = userId,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    PinCode = request.PinCode,
                    District = request.District,
                    HouseAddress = request.HouseAddress,
                    RoadName = request.RoadName
                };

                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>(true, "Address added successfully", new
                {
                    address = new
                    {
                        address.Id,
                        address.FullName,
                        address.PhoneNumber,
                        address.PinCode,
                        address.District,
                        address.HouseAddress,
                        address.RoadName
                    }
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }


        // PUT: api/address/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateAddress(int id, [FromBody] AddressRequest request)
        {
            try
            {
                var userId = GetUserId();
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

                if (address == null)
                {
                    return NotFound(new ApiResponse<object>(false, "Address not found", null));
                }

                address.PinCode = request.PinCode;
                address.District = request.District;
                address.HouseAddress = request.HouseAddress;
                address.RoadName = request.RoadName;
                address.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>(true, "Address updated successfully", new
                {
                    address = new
                    {
                        address.Id,
                        address.FullName,
                        address.PhoneNumber,
                        address.PinCode,
                        address.District,
                        address.HouseAddress,
                        address.RoadName
                    }
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        // DELETE: api/address/{id}


        // DELETE: api/address/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAddress(int id)
        {
            try
            {
                var userId = GetUserId();
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId && !a.IsDeleted);

                if (address == null)
                {
                    return NotFound(new ApiResponse<object>(false, "Address not found", null));
                }

                // HARD DELETE - physically removes from database
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object>(true, "Address deleted successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }
    }
}