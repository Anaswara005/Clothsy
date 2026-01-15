using Clothsy.Data;
using Clothsy.DTOs;
using Clothsy.DTOs.DonationDTOs;
using Clothsy.Models;
using Clothsy.Models.Donation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Clothsy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DonationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/donations - Create new donation
        [HttpPost]
        public async Task<IActionResult> CreateDonation(
            [FromForm] CreateDonationRequest request,
            [FromForm] List<IFormFile>? images)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Find hub by district
            var hub = await _context.Hubs
                .FirstOrDefaultAsync(h => h.District == request.District && h.IsActive);

            if (hub == null)
                return BadRequest(new { success = false, message = "No hub available for this district" });

            var donationId = GenerateDonationId();
            var title = $"{request.Category}'s {request.ClothingType}";

            var donation = new Donation
            {
                DonationId = donationId,
                DonorUserId = userId,
                Title = title,
                Category = request.Category,
                ClothingType = request.ClothingType,
                Size = request.Size,
                Description = request.Description,
                District = request.District,
                AssignedHubId = hub.Id,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            // ————————————————————————————————
            // 🔥 HANDLE IMAGE UPLOADS (New Code)
            // ————————————————————————————————
            var uploadedUrls = new List<string>();

            if (images != null && images.Count > 0)
            {
                Directory.CreateDirectory("wwwroot/uploads");

                int index = 0;

                foreach (var file in images.Take(3))
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}.jpg";
                        var path = Path.Combine("wwwroot/uploads", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var url = $"http://10.0.2.2:7269/uploads/{fileName}";

                        uploadedUrls.Add(url);

                        _context.DonationImages.Add(new DonationImage
                        {
                            DonationId = donation.Id,
                            ImageUrl = url,
                            IsPrimary = index == 0
                        });

                        if (index == 0)
                            donation.ThumbnailImageUrl = url;

                        index++;
                    }
                }
            }
            else
            {
                // 🔥 Fallback to photo URL fields if no file upload
                if (!string.IsNullOrEmpty(request.Photo1Url))
                {
                    uploadedUrls.Add(request.Photo1Url);

                    _context.DonationImages.Add(new DonationImage
                    {
                        DonationId = donation.Id,
                        ImageUrl = request.Photo1Url,
                        IsPrimary = true
                    });

                    donation.ThumbnailImageUrl = request.Photo1Url;
                }

                if (!string.IsNullOrEmpty(request.Photo2Url))
                {
                    uploadedUrls.Add(request.Photo2Url);

                    _context.DonationImages.Add(new DonationImage
                    {
                        DonationId = donation.Id,
                        ImageUrl = request.Photo2Url,
                        IsPrimary = false
                    });
                }

                if (!string.IsNullOrEmpty(request.Photo3Url))
                {
                    uploadedUrls.Add(request.Photo3Url);

                    _context.DonationImages.Add(new DonationImage
                    {
                        DonationId = donation.Id,
                        ImageUrl = request.Photo3Url,
                        IsPrimary = false
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Donation Created Successfully!",
                data = new
                {
                    donationId = donation.DonationId,
                    title = donation.Title,
                    categoryType = $"{donation.Category} - {donation.ClothingType}",
                    size = donation.Size,
                    district = donation.District,
                    status = donation.Status,
                    createdOn = donation.CreatedAt.ToString("dd-MM-yyyy"),
                    hubInfo = new
                    {
                        hub.Name,
                        hub.Address,
                        hub.Email,
                        hub.Phone,
                        hub.WorkingHours
                    }
                }
            });
        }



        // GET: api/donations/mydonations - Get my donations list
        [HttpGet("mydonations")]
        public async Task<IActionResult> GetMyDonations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donations = await _context.Donations
                .Include(d => d.Images) // 🔥 INCLUDE IMAGES
                .Where(d => d.DonorUserId == userId)
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.DonationId,
                    title = $"{d.Category} - {d.ClothingType}",
                    d.Size,
                    d.District,
                    d.Status,
                    d.CreatedAt,

                    // ✅ FIRST IMAGE FOR LIST
                    images = d.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => i.ImageUrl)
                        .ToList()
                })
                .ToListAsync();

            return Ok(new { success = true, data = donations });
        }


        // GET: api/donations/{id} - Get donation details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonationDetails(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donation = await _context.Donations
                .Include(d => d.AssignedHub)
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == id && d.DonorUserId == userId);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

            return Ok(new
            {
                success = true,
                data = new
                {
                    donation.Id,
                    donation.DonationId,
                    donation.Title,
                    donation.Category,
                    donation.ClothingType,
                    donation.Size,
                    donation.District,
                    donation.Description,
                    donation.Status,
                    images = donation.Images.Select(i => i.ImageUrl).ToList(),
                    createdOn = donation.CreatedAt.ToString("dd-MM-yyyy"),
                    hubInfo = new
                    {
                        donation.AssignedHub!.Name,
                        donation.AssignedHub.Address,
                        donation.AssignedHub.Email,
                        donation.AssignedHub.Phone,
                        donation.AssignedHub.WorkingHours
                    }
                }
            });
        }

        // DELETE: api/donations/{id} - Delete donation (only if Pending)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.Id == id && d.DonorUserId == userId);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

            if (donation.Status != "Pending")
                return BadRequest(new { success = false, message = "Cannot delete donation already in process" });

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Donation deleted successfully" });
        }
        // GET: api/donations/approved - Show approved donations to homepage users
        // GET: api/donations/approved
        // GET: api/donations/approved
        [HttpGet("approved")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApprovedDonations()
        {
            // Get user ID if authenticated (for excluding own donations)
            var userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                userId = int.Parse(userIdClaim);
            }

            var donations = await _context.Donations
                .Include(d => d.Images)
                .Where(d => d.Status == "Approved" && d.IsAvailable) // ✅ Single check
                .Where(d => d.DonorUserId != userId) // ✅ Exclude own donations
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    id = d.Id,
                    d.DonationId,
                    title = d.Title,
                    category = d.Category,
                    size = d.Size,
                    district = d.District,
                    thumbnail = d.ThumbnailImageUrl,
                    createdOn = d.CreatedAt.ToString("dd-MM-yyyy")
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Approved donation list",
                data = donations
            });
        }
        // PUT: api/donations/{id}/status - Update donation status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateDonationStatus(int id, [FromBody] StatusUpdateRequest request)
        {
            var donation = await _context.Donations.FindAsync(id);

            if (donation == null)
                return NotFound(new { success = false, message = "Donation not found" });

           
            donation.Status = request.Status;

            if (request.Status == "Approved")
            {
                donation.ApprovedAt = DateTime.UtcNow;

                // 🔥 THIS IS THE MISSING LINE
                donation.IsAvailable = true;
            }
            else if (request.Status == "Rejected")
            {
                donation.RejectedAt = DateTime.UtcNow;
                donation.IsAvailable = false;
            }

            await _context.SaveChangesAsync();


            return Ok(new { success = true, message = "Status updated successfully" });
        }

        // GET: api/donations/public/{id}
        // 🔥 For homepage item details (PUBLIC)
        [HttpGet("public/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicDonationDetails(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.Images)
                .Include(d => d.AssignedHub)
                .Where(d => d.Id == id && d.Status == "Approved")
                .Select(d => new
                {
                    id = d.Id,
                    donationId = d.DonationId,
                    title = d.Title,
                    category = d.Category,
                    clothingType = d.ClothingType,
                    size = d.Size,
                    description = d.Description,
                    status = d.Status,
                    hubName = d.AssignedHub != null ? d.AssignedHub.Name : null,
                    images = d.Images
                        .OrderByDescending(i => i.IsPrimary)
                        .Select(i => new
                        {
                            imageUrl = i.ImageUrl
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (donation == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Item not found or not approved"
                });
            }

            return Ok(new
            {
                success = true,
                data = donation
            });
        }
        // POST: api/donations/{id}/request - Request an item
        [HttpPost("{id}/request")]
        public async Task<IActionResult> RequestItem(int id, [FromBody] RequestItemDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Check if donation exists and is approved
            var donation = await _context.Donations
                .Include(d => d.AssignedHub)
                .FirstOrDefaultAsync(d => d.Id == id && d.Status == "Approved" && d.IsAvailable);

            if (donation == null)
                return NotFound(new { success = false, message = "Item not found or not available" });

            // Check if user's address exists
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == request.AddressId && a.UserId == userId);

            if (address == null)
                return BadRequest(new { success = false, message = "Invalid address" });

            // Check if user already has a pending request for this item
            var existingRequest = await _context.DonationRequests
                .FirstOrDefaultAsync(r => r.DonationId == id && r.RequesterId == userId && r.Status == "Pending");

            if (existingRequest != null)
                return BadRequest(new { success = false, message = "You already have a pending request for this item" });

            // Create the request
            var donationRequest = new DonationRequest
            {
                DonationId = id,
                RequesterId = userId,
                AddressId = request.AddressId,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };

          
            _context.DonationRequests.Add(donationRequest);
            donation.IsAvailable = false;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = new
                {
                    requestId = $"REQ-{donationRequest.Id}",
                    hub = donation.AssignedHub == null
            ? null
            : new
            {
                name = donation.AssignedHub.Name,
                address = donation.AssignedHub.Address,
                email = donation.AssignedHub.Email,
                phone = donation.AssignedHub.Phone,
                workingHours = donation.AssignedHub.WorkingHours
            }
                }
            });

        }


        [HttpGet("myrequests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var requests = await _context.DonationRequests
                .Where(r => r.RequesterId == userId)
                .Join(
                    _context.Donations,
                    r => r.DonationId,
                    d => d.Id,
                    (r, d) => new
                    {
                        id = r.Id,
                        itemName = d.Title,
                        category = d.Category,
                        size = d.Size,
                        imageUrl = d.ThumbnailImageUrl,
                        status = r.Status,
                        requestedAt = r.RequestedAt
                    }
                )
                .ToListAsync();

            return Ok(new { success = true, data = requests });
        }



        [HttpDelete("requests/{id}")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var request = await _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefaultAsync(r => r.Id == id && r.RequesterId == userId);

            if (request == null)
                return NotFound(new { message = "Request not found" });

            if (request.Status != "Pending")
                return BadRequest(new { message = "Cannot cancel non-pending request" });

            // 🔥 CRITICAL FIX
            request.Donation!.IsAvailable = true;

            _context.DonationRequests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Request cancelled" });
        }
        // GET: api/donations/requests/{id}/details
        // GET: api/donations/requests/{id}/details
        [HttpGet("requests/{id}/details")]
        public async Task<IActionResult> GetRequestDetails(int id)
        {
            Console.WriteLine($"🔍 GetRequestDetails called with ID: {id}");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            Console.WriteLine($"👤 User ID: {userId}");

            var request = await _context.DonationRequests
                .Where(r => r.Id == id && r.RequesterId == userId)
                .Join(
                    _context.Donations
                        .Include(d => d.Images)
                        .Include(d => d.AssignedHub),
                    r => r.DonationId,
                    d => d.Id,
                    (r, d) => new
                    {
                        id = r.Id,
                        itemName = d.Title,
                        category = d.Category,
                        size = d.Size,
                        description = d.Description,
                        status = r.Status,
                        requestedAt = r.RequestedAt,
                        images = d.Images
                            .OrderByDescending(i => i.IsPrimary)
                            .Select(i => i.ImageUrl)
                            .ToList(),
                        hubInfo = d.AssignedHub == null
    ? null
    : new
    {
        name = d.AssignedHub.Name,
        address = d.AssignedHub.Address,
        email = d.AssignedHub.Email,
        phone = d.AssignedHub.Phone,
        workingHours = d.AssignedHub.WorkingHours
    }

                    }
                )
                .FirstOrDefaultAsync();

            if (request == null)
            {
                Console.WriteLine($"❌ Request not found for ID: {id}");
                return NotFound(new { success = false, message = "Request not found" });
            }

            Console.WriteLine($"✅ Request found: {request.itemName}");
            return Ok(new { success = true, data = request });
        }

        // Helper method to generate unique donation ID
        private string GenerateDonationId()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return $"CLO-{timestamp}";
        }
    }

    public class StatusUpdateRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }


}

