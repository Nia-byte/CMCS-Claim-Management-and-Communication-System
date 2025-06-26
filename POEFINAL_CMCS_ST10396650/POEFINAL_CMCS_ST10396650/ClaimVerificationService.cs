using System.Security.Claims;

namespace POEFINAL_CMCS_ST10396650
{
    public class ClaimVerificationService 
    {
        private readonly ApplicationDbContext _context;

        public ClaimVerificationService(ApplicationDbContext context)
        {
            _context = context;
        }

      

       
        public bool ValidateClaim(ClaimModel claim)
        {
            var validationErrors = new List<string>();

            if (claim.HoursWorked > 40)
                validationErrors.Add("Hours exceeded 40");

            if (claim.HourlyRate > 1000 || claim.HourlyRate <= 0)
                validationErrors.Add("Invalid hourly rate");

            return validationErrors.Count == 0;
        }
    }
}
