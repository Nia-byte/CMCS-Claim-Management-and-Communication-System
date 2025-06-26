using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class PCoordinatorModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<POEFINAL_CMCS_ST10396650.ClaimModel> Claims { get; set; }

        public PCoordinatorModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<POEFINAL_CMCS_ST10396650.ReviewedClaim> FinalApprovedClaims { get; set; }
        public List<ClaimModel> ReviewedClaims { get; set; }
        public string SelectedClaimDetails { get; set; }

        public IActionResult OnGet()
        {
            // Fetch coordinator ID from claims principal or session
            var coordinatorId = GetCurrentCoordinatorId();

            Claims =  _context.Claims
                  .Include(c => c.Lecturer)
                  .ToList();

            FinalApprovedClaims = _context.ReviewedClaims
               .Include(c => c.Claim)
               .Include(c => c.Lecturer)
               .Include(c => c.Coordinator)
               .ToList();


            return Page();
        }

        private int GetCurrentCoordinatorId()
        {
            // Implement logic to retrieve current coordinator's ID
            // This could be from User.Identity, Session, or other authentication method
            // For now, return a placeholder
            return 1; // Replace with actual logic
        }

        // Optional: Add method to handle claim navigation
        public IActionResult OnGetClaimDetails(int claimId)
        {
            var claim = _context.Claims
                .Include(c => c.Lecturer)
                .FirstOrDefault(c => c.ClaimId == claimId);

            if (claim == null)
                return NotFound();

            SelectedClaimDetails = $"Lecturer: {claim.Lecturer.fullName}\n" +
                                   $"Submission Date: {claim.SubmissionDate:dd/MM/yyyy}\n" +
                                   $"Hours: {claim.HoursWorked}\n" +
                                   $"Total Amount: {claim.Total:C}";

            return Page();
        }
        public IActionResult OnPostApprove(int claimId)
        {
            try
            {
                var reviewedClaim = _context.Claims
                    .FirstOrDefault(c => c.ClaimId == claimId);

                if (reviewedClaim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                reviewedClaim.Status = "Approved";
                _context.Claims.Update(reviewedClaim);
                _context.SaveChanges();

                TempData["Success"] = "Claim has been approved successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error approving claim: {ex.Message}";
                return RedirectToPage();
            }
        }

        public IActionResult OnPostReject(int claimId, string reason)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);
            claim.Status = "Approved";
            claim.Notes = reason;
            _context.Claims.Update(claim);
            _context.SaveChanges();

           

            // Save all changes in a single transaction
            _context.SaveChanges();

            return RedirectToPage();
        }
    }

    // Define a simple view model for claims
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}