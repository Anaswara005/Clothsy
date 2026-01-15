using Clothsy.Models.SignupModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models
{
    public class NotificationRead
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime LastReadAt { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}