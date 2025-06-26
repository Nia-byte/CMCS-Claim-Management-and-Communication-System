using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        [Required]
        [ForeignKey("Coordinator")]
        public int CoordinatorId { get; set; }

        [Required]
        public int ReviewedClaimId { get; set; }
//(LearnRazorPages,[s.a])
        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; } = false;  

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;  
//(Endjin, 2022)
       
        public virtual ProgrammeCoordinator Coordinator { get; set; }
       
        [ForeignKey("ReviewedClaimId")]
        public virtual ReviewedClaim ReviewedClaim { get; set; }
    }
}
