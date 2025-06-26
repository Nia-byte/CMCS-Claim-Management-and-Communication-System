using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class ManagerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<POEFINAL_CMCS_ST10396650.ClaimModel> Claims { get; set; }
        public int TotalClaims { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalAmount { get; set; }

        public ManagerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // (MicroSoftLearn, 2024)
            Claims = _context.Claims
             .Include(c => c.Lecturer)
             .Where(c => c.Status == "Approved")
             .ToList();

            TotalClaims = Claims.Count;
            TotalHours = Claims.Sum(c => c.HoursWorked);
            TotalAmount = Claims.Sum(c => c.Total);

            return Page();
        }
    }
}
