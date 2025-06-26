using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class HRManagementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
//(MicroSoftLearn, 2024)
        [BindProperty]
        public List<POEFINAL_CMCS_ST10396650.ClaimModel> Claims { get; set; }

        [BindProperty]
        public Lecture NewLecturer { get; set; }

        public List<Lecture> Lecturers { get; set; }

        public int TotalClaims { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalAmount { get; set; }

        public HRManagementModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            Lecturers = _context.Lecturers.ToList();
        }
//(LearnRazorPages,[s.a])
        public async Task OnGetAsync()
        {
            Claims = await _context.Claims
                .Include(c => c.Lecturer)
                .ToListAsync(); 

            
           Lecturers = await _context.Lecturers.ToListAsync();
           
            TotalClaims = Claims.Count;
            TotalHours = Claims.Sum(c => c.HoursWorked);
            TotalAmount = Claims.Sum(c => c.Total);
        }
//(MicroSoftLearn, 2024) 
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Lecturers = _context.Lecturers.ToList();
                return Page();
            }

            _context.Lecturers.Add(NewLecturer);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostGenerateReportAsync(DateTime StartDate, DateTime EndDate)
        {//(Twilio, 2020)
            var claims = await _context.Claims
                .Include(c => c.Lecturer)
                .Where(c => c.Status == "Approved" )
                .ToListAsync();

            // Create reports directory if it doesn't exist
            string reportDirectory = Path.Combine(_environment.WebRootPath, "reports");
            Directory.CreateDirectory(reportDirectory);

            // The  filename is going to generate a unique filename
            string filename = $"PaymentReport_{StartDate:yyyyMM}_{DateTime.Now:HHmmss}.txt";
            string filePath = Path.Combine(reportDirectory, filename);

            // Use StreamWriter to generate the report
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write report header
                writer.WriteLine("**Monthly Payment Report**");
                writer.WriteLine("_____________________________________________________");
                writer.WriteLine($"**Generated Date:** {DateTime.Now:dd MMMM yyyy}");
                writer.WriteLine($"**Report Period:** {StartDate:dd MMMM yyyy} - {EndDate:dd MMMM yyyy}\n");

               
                writer.WriteLine("_____________________________________________________"); // Empty line between headers and data

                // Write claim details
                foreach (var claim in claims)
                {
                    writer.WriteLine($"Lecture Name:    {claim.Lecturer.fullName}, " + "\n" +
                                     $"Hours Worked:    {claim.HoursWorked:N2}, " + "\n" +
                                     $"Hourly Rate (R): {claim.HourlyRate:N2}, " + "\n" +
                                     $"Month:           {claim.Month}, " + "\n" +
                                     $"Module:          {claim.Module}, " + "\n" +
                                     $"*Total:         R{claim.Total:N2}" +
                                     "_____________________________________________________");
                }

                // Write summary
                writer.WriteLine("\n**Summary**");
                writer.WriteLine($"**Total Claims Processed:** {claims.Count}");
                writer.WriteLine($"**Total Hours Worked:** {claims.Sum(c => c.HoursWorked):N2}");
                writer.WriteLine($"**Total Amount:** R{claims.Sum(c => c.Total):N2}");
            }

            // Return file for download
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "text/plain", filename);
        }
    }
}
//(MicroSoftLearn, 2024)
