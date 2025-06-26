using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string[] Role { get; set; }

        public IActionResult OnPost()
        {
            if (Role == null || Role.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a role.");
                return Page();
            }

            var selectedRole = Role[0]; // Assume only one role is allowed per login


            switch (selectedRole)
            {
                case "Lecturer":
                    var lecturer = _dbContext.Lecturers
                      .FirstOrDefault(l => l.username == Username && l.password == Password);

                    return RedirectToPage("/Lecture", new { id = lecturer.lecturerId });


                case "Coordinator":
                    var coordinator = _dbContext.ProgrammeCoordinator
                        .FirstOrDefault(c => c.fullName == Username && c.password == Password);

                    return RedirectToPage("/PCoordinator", new { id = coordinator.CoordinatorId });
                case "Manager":
                    var manager = _dbContext.AcademicManager
                        .FirstOrDefault(m => m.fullName == Username && m.password == Password);

                    return RedirectToPage("/Manager", new { id = manager.ManagerId });
                case "HRManagement":

                    return RedirectToPage("/HRManagement");
                default:
                    ModelState.AddModelError(string.Empty, "Invalid role selected.");
                    return Page();
            }
        }
    }
}
