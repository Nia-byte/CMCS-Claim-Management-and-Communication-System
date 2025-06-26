using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class FinalApprovalsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FinalApprovalsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<POEFINAL_CMCS_ST10396650.ReviewedClaim> FinalApprovedClaims { get; set; }

        public IActionResult OnGet(int? lecturerId)
        {
            if (lecturerId == null)
            {
                return NotFound();
            }

            FinalApprovedClaims = _context.ReviewedClaims
                .Include(c => c.Claim)
                .Include(c => c.Lecturer)
                .Include(c => c.StatusApproval)
                .ToList();


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
                var reviewedClaim = _context.Claims
                    .FirstOrDefault(c => c.ClaimId == claimId);

                if (reviewedClaim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                reviewedClaim.Status = "Rejected";
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
    }
}

