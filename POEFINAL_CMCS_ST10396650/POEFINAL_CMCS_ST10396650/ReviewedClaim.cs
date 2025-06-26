using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace POEFINAL_CMCS_ST10396650
{
    public class ReviewedClaim
    {
        [Key]
        public int ReviewId { get; set; }
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public int CoordinatorId { get; set; }
        public string Status { get; set; }
        public string StatusApproval { get; set; }
        public DateTime ReviewedDate { get; set; }
      

        // Navigation properties
        public virtual ClaimModel Claim { get; set; }
        public virtual Lecture Lecturer { get; set; }
        public virtual ProgrammeCoordinator Coordinator { get; set; }
    }
}

