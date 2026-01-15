using Clothsy.Models.SignupModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothsy.Models.Profile
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }


        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string? PinCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string? District { get; set; }

        [Required]
        [MaxLength(200)]
        public string? HouseAddress { get; set; }

        [MaxLength(200)]
        public string? RoadName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}