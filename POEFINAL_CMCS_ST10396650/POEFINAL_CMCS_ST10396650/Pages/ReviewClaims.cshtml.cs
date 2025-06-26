using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class ReviewClaimsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ReviewedClaim> PendingClaims { get; set; }

        public ReviewClaimsModel(ApplicationDbContext context)
        {
            _context = context;
        }
//(Troelsen & Japikse, 2022)
        public IActionResult OnGet()
        {
            PendingClaims = _context.ReviewedClaims
                .Include(c => c.Claim)
                .Include(c => c.Lecturer)
                .Include(c => c.Coordinator)
                .Where(c => c.StatusApproval == "Waiting for Approval")
                .ToList();

            return Page();
        }


        public IActionResult OnPostApprove(int claimId)
        {
            try
            {
                var reviewedClaim = _context.ReviewedClaims
                    .FirstOrDefault(c => c.ClaimId == claimId);

                if (reviewedClaim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                reviewedClaim.StatusApproval = "Approved";
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

        public IActionResult OnPostReject(int claimId)
        {
            try
            {
                var reviewedClaim = _context.ReviewedClaims
                    .FirstOrDefault(c => c.ClaimId == claimId);

                if (reviewedClaim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                reviewedClaim.StatusApproval = "Rejected";
                _context.SaveChanges();

                TempData["Success"] = "Claim has been rejected successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error rejecting claim: {ex.Message}";
                return RedirectToPage();
            }
        }
        public JsonResult OnGetCheckModuleClaim(int claimId, string module, string month)
        {
            try
            {
                // Check if there's already a claim for this module and month
                var existingClaim = _context.Claims
                    .Any(c => c.ClaimId != claimId &&
                             c.Module == module &&
                             c.Month == month &&
                             c.Status != "Rejected");

                return new JsonResult(new { isValid = !existingClaim });
            }
            catch (Exception)
            {
                return new JsonResult(new { isValid = false });
            }
        }
    }
}
//(Troelsen & Japikse, 2022)
