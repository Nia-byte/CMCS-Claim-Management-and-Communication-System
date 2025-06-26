using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class PendingClaimsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<POEFINAL_CMCS_ST10396650.ClaimModel> PendingClaims { get; set; }


        [BindProperty]
        public int CurrentCoordinatorId { get; set; }

        public PendingClaimsModel(ApplicationDbContext context)
        {
            _context = context;
            PendingClaims = new List<POEFINAL_CMCS_ST10396650.ClaimModel>();

            CurrentCoordinatorId = 1;
        }
//(LearnRazorPages,[s.a])
        
        public IActionResult OnGet()
        {
            try
            {
                CurrentCoordinatorId = GetCurrentCoordinatorId();
                if (CurrentCoordinatorId == 0)
                {
                    TempData["Error"] = "No valid coordinator found in the system.";
                    return RedirectToPage();
                }
               

                PendingClaims = _context.Claims
                    .Where(c => c.Status == "Pending" &&
                               !_context.ReviewedClaims.Any(rc => rc.ClaimId == c.ClaimId))
                    .Select(c => new POEFINAL_CMCS_ST10396650.ClaimModel
                    {
                        ClaimId = c.ClaimId,
                        Module = c.Module,
                        HourlyRate = c.HourlyRate,
                        HoursWorked = c.HoursWorked,
                        Total = c.Total,
                        Status = c.Status,
                        SubmissionDate = c.SubmissionDate,
                        Notes = c.Notes,
                        LecturerId = c.LecturerId
                    })
                    .ToList();


                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading pending claims: {ex.Message}";
                return Page();
            }
        }

        public IActionResult OnPostApprove(int claimId, string reason)
        {
            try
            {
                
                System.Diagnostics.Debug.WriteLine($"Starting approval process for claim {claimId}");

                // Validate claim exists
                var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);
                if (claim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                var coordinator = _context.ProgrammeCoordinator.FirstOrDefault(p => p.CoordinatorId == CurrentCoordinatorId);
                if (coordinator == null)
                {
                    TempData["Error"] = $"Coordinator with ID {CurrentCoordinatorId} not found.";
                    return RedirectToPage();
                }

              
                System.Diagnostics.Debug.WriteLine($"Found claim: {claimId}, LecturerId: {claim.LecturerId}");

                // Validate inputs
                if (CurrentCoordinatorId <= 0)
                {
                    TempData["Error"] = "Invalid Coordinator ID";
                    return RedirectToPage();
                }

                
                System.Diagnostics.Debug.WriteLine($"Current Coordinator ID: {CurrentCoordinatorId}");

             
                System.Diagnostics.Debug.WriteLine($"Database path: {_context.Database.GetConnectionString()}");

                try
                {
                    
                    var reviewedClaim = new ReviewedClaim
                    {
                        ClaimId = claimId,
                        LecturerId = claim.LecturerId,
                        CoordinatorId = CurrentCoordinatorId,
                        Status = "Approved",
                        StatusApproval = "Waiting for Approval",
                        ReviewedDate = DateTime.Now
                       
                    };

                    // Debug log 4
                    System.Diagnostics.Debug.WriteLine($"Created ReviewedClaim object: ClaimId={reviewedClaim.ClaimId}, " +
                        $"LecturerId={reviewedClaim.LecturerId}, CoordinatorId={reviewedClaim.CoordinatorId}");

                   

                    // Debug log 5
                    System.Diagnostics.Debug.WriteLine("Updated original claim");

                    // Then add the reviewed claim
                    _context.ReviewedClaims.Add(reviewedClaim);

                    // Debug log 6
                    System.Diagnostics.Debug.WriteLine("Added reviewed claim to context");

                    // Save changes
                    var entriesBefore = _context.ChangeTracker.Entries().Count();
                    System.Diagnostics.Debug.WriteLine($"Entries before save: {entriesBefore}");

                    _context.SaveChanges();

                    var entriesAfter = _context.ChangeTracker.Entries().Count();
                    System.Diagnostics.Debug.WriteLine($"Entries after save: {entriesAfter}");

                    TempData["Success"] = "Claim has been approved and is waiting for final approval.";
                    return RedirectToPage();
                } //(LearnRazorPages,[s.a])
                catch (DbUpdateException ex)
                {
                    // Log the inner exception if it exists
                    if (ex.InnerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                        TempData["Error"] = $"Database Error: {ex.InnerException.Message}";
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Database Exception: {ex.Message}");
                        TempData["Error"] = $"Database Error: {ex.Message}";
                    }
                    return RedirectToPage();
                }//(LearnRazorPages,[s.a])
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Exception: {ex.Message}");
                TempData["Error"] = $"Error approving claim: {ex.Message}";
                return RedirectToPage();
            }
        }
//(MicroSoftLearn, 2024)
        public IActionResult OnPostReject(int claimId, string reason)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);
                if (claim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return RedirectToPage();
                }

                var reviewedClaim = new ReviewedClaim
                {
                    ClaimId = claimId,
                    LecturerId = claim.LecturerId,
                    CoordinatorId = CurrentCoordinatorId,
                    Status = "Rejected",
                    StatusApproval = "Waiting for Approval",
                    ReviewedDate = DateTime.Now
                   
                };

                _context.ReviewedClaims.Add(reviewedClaim);
                _context.SaveChanges();

                TempData["Success"] = "Claim has been rejected and is waiting for final approval.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error rejecting claim: {ex.Message}";
                return RedirectToPage();
            }
        }
        private int GetCurrentCoordinatorId()
        {
            var coordinator = _context.ProgrammeCoordinator.FirstOrDefault();
            return coordinator?.CoordinatorId ?? 0;// Replace this with actual implementation
        }

        private bool VerifyDatabaseConnection()
        {
            try
            {
                // Try to execute a simple query
                var testQuery = _context.Claims.FirstOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection test failed: {ex.Message}");
                return false;
            }
        }

        public JsonResult OnGetCheckModuleClaim(int claimId, string module, string month)
        {
            try
            {
                // Check if there's already a claim for this module and month
                var PendingClaims = _context.Claims
                    .Any(c => c.ClaimId != claimId &&
                             c.Module == module &&
                             c.Month == month &&
                             c.Status != "Rejected");

                return new JsonResult(new { isValid = !PendingClaims });
            }
            catch (Exception)
            {
                return new JsonResult(new { isValid = false });
            }
        }
        public IActionResult OnGetDownloadDocument(int claimId)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);
                if (claim?.Document == null)
                {
                    return NotFound("Document not found.");
                }

                // Determine the file type - you might want to store this in the database
                string contentType = "application/pdf"; // Default to PDF
                string fileName = $"Claim_{claimId}_Document.pdf";

                return File(claim.Document, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading document: {ex.Message}");
            }
        }
    }
    //(Troelsen & Japikse, 2022)
}
