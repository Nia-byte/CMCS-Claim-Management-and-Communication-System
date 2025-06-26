namespace POEFINAL_CMCS_ST10396650
{
    public class Claim
    {
        // Parameterless constructor required by EF Core
        public Claim()
        {
        }

        // Primary key
        public int ClaimId { get; set; }

        // Required properties
        public string Module { get; set; }
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Notes { get; set; }
        public int LecturerId { get; set; }

        // Navigation property (if you need it)
        public virtual Lecture Lecturer { get; set; }
    }
}
