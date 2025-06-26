using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Security.Claims;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class SubmitClaimModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SubmitClaimModel> _logger;

        public SubmitClaimModel(ApplicationDbContext context, ILogger<SubmitClaimModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ClaimSubmission Input { get; set; }

        public class ClaimSubmission
        {
            [Required]
            public int LecturerId { get; set; }

            [Required(ErrorMessage = "Month is required")]
            [StringLength(50)]
            public string Month { get; set; }

            [Required(ErrorMessage = "Hours worked is required")]
            [Range(0, 48, ErrorMessage = "Hours must be between 0 and 48")]
            public decimal HoursWorked { get; set; }

            [Required(ErrorMessage = "Hourly rate is required")]
            [Range(0, 1000, ErrorMessage = "Hourly rate must be between 0 and 1000")]
            public decimal HourlyRate { get; set; } = 200;

            public decimal Total { get; set; }

            public string Notes { get; set; }

            public IFormFile DocumentUpload { get; set; }
        }

        public List<string> Months { get; set; } = new List<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public IActionResult OnGet()
        {
            // Initialize with current lecturer ID
            Input = new ClaimSubmission
            {
                LecturerId = GetCurrentLecturerId(),
                HourlyRate = 200 // Fixed hourly rate
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                byte[] fileContent = null;
                if (Input.DocumentUpload != null && Input.DocumentUpload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Input.DocumentUpload.CopyToAsync(memoryStream);
                        fileContent = memoryStream.ToArray();
                    }
                }
//(Troelsen & Japikse, 2022)
                decimal total = Input.HoursWorked * Input.HourlyRate;

                // Use raw SQL insert
                string sqlQuery = @"
            INSERT INTO Claims 
            (LecturerId, HoursWorked, HourlyRate, Status, Notes, SubmissionDate, Month, Total, Document) 
            VALUES 
            (@LecturerId, @HoursWorked, @HourlyRate, @Notes, @SubmissionDate, @Month, @Total, @Document)";

                var parameters = new[]
                {
            new SqlParameter("@LecturerId", 1),
            new SqlParameter("@Month", Input.Month),
            new SqlParameter("@HoursWorked", Input.HoursWorked),
            new SqlParameter("@HourlyRate", Input.HourlyRate),
            new SqlParameter("@Total", total),
            new SqlParameter("@Status", "Pending"),
            new SqlParameter("@Notes", (object)Input.Notes ?? DBNull.Value),
            new SqlParameter("@SubmissionDate", DateTime.Now),
            new SqlParameter("@Document", (object)fileContent ?? DBNull.Value)
        };

                // Execute the raw SQL insert
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(sqlQuery, parameters);

                if (rowsAffected > 0)
                {
                    // Successful insertion
                    TempData["SuccessMessage"] = "Claim submitted successfully!";
                    return RedirectToPage("/Lecturer");
                }
                else
                {
                    // Failed insertion
                    ModelState.AddModelError(string.Empty, "Failed to submit claim.");
                    return Page();
                }


                // Generate Claim Summary
                var claimSummary = _context.Claims
                    .Where(c => c.LecturerId == Input.LecturerId)
                    .GroupBy(c => c.Status)
                    .Select(g => new
                    {
                        Status = g.Key,
                        TotalClaims = g.Count(),
                        TotalHours = g.Sum(c => c.HoursWorked),
                        TotalAmount = g.Sum(c => c.Total)
                    })
                    .ToList(); // Remove await here

                TempData["ClaimSummary"] = string.Join("\n", claimSummary.Select(cs =>
                    $"Status: {cs.Status}\n" +
                    $"Total Claims: {cs.TotalClaims}\n" +
                    $"Total Hours: {cs.TotalHours}\n" +
                    $"Total Amount: {cs.TotalAmount:C}\n"
                ));

                return RedirectToPage("/Lecturer");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting claim");
                ModelState.AddModelError(string.Empty, "An error occurred while submitting the claim.");
                return Page();
            }
        }

        private int GetCurrentLecturerId()
        {
            // This is a placeholder. In a real app, you'd get the ID from the authenticated user
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}
//(Troelsen & Japikse, 2022)
