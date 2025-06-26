using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POEFINAL_CMCS_ST10396650
{
    public class ClaimModel
    {

        public ClaimModel()
        {
        }
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Month { get; set; }

        [Required]
        [Range(0, 40, ErrorMessage = "Hours worked must be between 0 and 40 per week")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Hourly rate must be between 0 and 1000.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; } = 200;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000, ErrorMessage = "Total claim amount must be reasonable")]
        public decimal Total { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public string Notes { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        // Rename UploadedDocument to Document
        public byte[] Document { get; set; }

        public string Module { get; set; }


        // Navigation property
        public virtual Lecture Lecturer { get; set; }
    }
    public class ClaimVerificationCriteria
    {
        // Maximum weekly hours allowed
        public const double MAX_WEEKLY_HOURS = 40;

        // Minimum supporting documentation required (in bytes)
        public const int MIN_DOCUMENT_SIZE = 1024; // 1 KB

        // Maximum claim amount per submission
        public const decimal MAX_CLAIM_AMOUNT = 5000M;

        // Minimum time between claims
        public const int MIN_DAYS_BETWEEN_CLAIMS = 7;

        // Hourly rate limits
        public const decimal MIN_HOURLY_RATE = 50M;
        public const decimal MAX_HOURLY_RATE = 200M;
    }
}
