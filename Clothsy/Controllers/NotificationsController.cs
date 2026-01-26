using Clothsy.Data;
using Clothsy.DTOs.HomepageDTOs;
using Clothsy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace Clothsy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/notifications - Get user's notifications
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Get last read timestamp from database
            var lastReadRecord = await _context.NotificationReads
                .FirstOrDefaultAsync(nr => nr.UserId == userId);

            DateTime? lastRead = lastReadRecord?.LastReadAt;

            // 🔍 DEBUG LOGGING
            Console.WriteLine($"📊 GetNotifications for UserId={userId}");
            Console.WriteLine($"📊 LastReadAt from DB: {lastRead?.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

            // Get notifications for donated items that were requested
            var donationNotifications = await _context.DonationRequests
                .Where(r => r.Donation!.DonorUserId == userId)
                .Include(r => r.Donation)
                .Include(r => r.Requester)
                .OrderByDescending(r => r.RequestedAt)
                .Take(50)
                .ToListAsync();

            // Get notifications for status changes on user's own requests
            var requestNotifications = await _context.DonationRequests
                .Where(r => r.RequesterId == userId && r.Status != "Pending")
                .Include(r => r.Donation)
                .OrderByDescending(r => r.RequestedAt)
                .Take(50)
                .ToListAsync();

            // Map to consistent notification format
            var allNotifications = new List<NotificationDto>();

            // Add donation request notifications
            foreach (var r in donationNotifications)
            {
                var isRead = lastRead.HasValue && r.RequestedAt <= lastRead.Value;
                
                // 🔍 DEBUG LOGGING
                Console.WriteLine($"  Notification ID={r.Id}, Type=request, RequestedAt={r.RequestedAt:yyyy-MM-dd HH:mm:ss.fff}, IsRead={isRead}");
                
                allNotifications.Add(new NotificationDto
                {
                    Id = r.Id,
                    Type = "request",
                    Title = "New Request",
                    Message = $"{r.Requester!.FullName} requested your {r.Donation!.Title}",
                    ItemImage = r.Donation.ThumbnailImageUrl,
                    CreatedAt = r.RequestedAt,
                    IsRead = isRead
                });
            }


            // Add status update notifications
            foreach (var r in requestNotifications)
            {
                var createdAt = r.Status == "Approved"
                    ? (r.ApprovedAt ?? r.RequestedAt)
                    : (r.RejectedAt ?? r.RequestedAt);

                var isRead = lastRead.HasValue &&
                    (
                        (r.Status == "Approved" &&
                         (r.ApprovedAt ?? r.RequestedAt) <= lastRead.Value) ||
                        (r.Status == "Rejected" &&
                         (r.RejectedAt ?? r.RequestedAt) <= lastRead.Value)
                    );

                // 🔍 DEBUG LOGGING
                Console.WriteLine($"  Notification ID={r.Id}, Type=status_update, CreatedAt={createdAt:yyyy-MM-dd HH:mm:ss.fff}, IsRead={isRead}");

                allNotifications.Add(new NotificationDto
                {
                    Id = r.Id,
                    Type = "status_update",
                    Title = r.Status == "Approved" ? "Request Approved" : "Request Rejected",
                    Message = $"Your request for {r.Donation!.Title} has been {r.Status.ToLower()}",
                    ItemImage = r.Donation.ThumbnailImageUrl,
                    CreatedAt = createdAt,
                    IsRead = isRead
                });
            }


            // Sort all notifications by date
            var sortedNotifications = allNotifications
                .OrderByDescending(n => n.CreatedAt)
                .ToList();


            var unreadCount = sortedNotifications.Count(n => !n.IsRead);

            // 🔍 DEBUG LOGGING
            Console.WriteLine($"📊 Total notifications: {sortedNotifications.Count}, Unread: {unreadCount}");

            return Ok(new
            {
                success = true,
                data = sortedNotifications,
                unreadCount = unreadCount
            });
        }

        // 🆕 PUT: api/notifications/mark-all-read - Mark all notifications as read
        [HttpPut("mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Add 5 second buffer to ensure all notifications up to this moment are marked as read
            // This prevents timing/precision issues with DateTime comparisons and database latency
            var readTimestamp = DateTime.UtcNow.AddSeconds(5);

            // 🔍 DEBUG LOGGING
            Console.WriteLine($"📝 MarkAllAsRead for UserId={userId}");
            Console.WriteLine($"📝 Setting LastReadAt={readTimestamp:yyyy-MM-dd HH:mm:ss.fff}");

            // Find or create the read record
            var readRecord = await _context.NotificationReads
                .FirstOrDefaultAsync(nr => nr.UserId == userId);

            if (readRecord == null)
            {
                readRecord = new NotificationRead
                {
                    UserId = userId,
                    LastReadAt = readTimestamp
                };
                _context.NotificationReads.Add(readRecord);
                Console.WriteLine($"📝 Created new NotificationRead record");
            }
            else
            {
                Console.WriteLine($"📝 Previous LastReadAt was: {readRecord.LastReadAt:yyyy-MM-dd HH:mm:ss.fff}");
                readRecord.LastReadAt = readTimestamp;
                _context.NotificationReads.Update(readRecord);
                Console.WriteLine($"📝 Updated LastReadAt to: {readTimestamp:yyyy-MM-dd HH:mm:ss.fff}");
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"📝 Changes saved to database");

            return Ok(new
            {
                success = true,
                message = "All notifications marked as read",
                timestamp = readTimestamp,
                unreadCount = 0 // After marking all as read, count should be 0
            });
        }

        // PUT: api/notifications/{id}/read - Mark notification as read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            // This is a placeholder - you'll need to add an IsRead column
            // to your DonationRequests table to properly implement this
            return Ok(new { success = true, message = "Marked as read" });
        }

        // DELETE: api/notifications/{id} - Delete notification
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var request = await _context.DonationRequests
                .Include(r => r.Donation)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound(new { success = false, message = "Notification not found" });

            // Check if user owns this notification
            if (request.Donation!.DonorUserId != userId && request.RequesterId != userId)
                return Forbid();

            return Ok(new { success = true, message = "Notification dismissed" });
        }
    }
}