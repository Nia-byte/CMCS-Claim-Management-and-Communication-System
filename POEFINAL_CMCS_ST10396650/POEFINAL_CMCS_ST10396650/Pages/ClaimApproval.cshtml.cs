using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class ClaimApprovalModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimVerificationService _verificationService;

        public ClaimApprovalModel(ApplicationDbContext context, ClaimVerificationService verificationService)
        {
            _context = context;
            _verificationService = verificationService;
        }

        [BindProperty]
        public POEFINAL_CMCS_ST10396650.ClaimModel Claim { get; set; } // Specify the full namespace

        [BindProperty]
        public string RejectionReason { get; set; }

        public IActionResult OnGet(int id)
        {
            Claim = _context.Claims
                .Include(c => c.Lecturer)
                .FirstOrDefault(c => c.ClaimId == id);

            if (Claim == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostApprove()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_verificationService.ValidateClaim(Claim))
            {
                ModelState.AddModelError("", "Claim validation failed");
                return Page();
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == Claim.ClaimId);
            if (claim != null)
            {
                claim.Status = "Pending Academic Manager Approval";
                _context.SaveChanges();
            }

            return RedirectToPage("/PendingClaims");
        }

        public IActionResult OnPostReject()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == Claim.ClaimId);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.Notes += $"\nRejection Reason: {RejectionReason}";
                _context.SaveChanges();
            }

            return RedirectToPage("/PendingClaims");
        }
    }
}
