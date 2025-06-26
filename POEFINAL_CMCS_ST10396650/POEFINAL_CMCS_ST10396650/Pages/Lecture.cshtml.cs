using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace POEFINAL_CMCS_ST10396650.Pages
{
    public class LectureModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public int LecturerId { get; set; }

        public List<ClaimHistory> ClaimsHistory { get; set; } = new List<ClaimHistory>();

        public int TotalClaims { get; set; }
        public int ApprovedClaims { get; set; }
        public string TrackedClaimDetails { get; set; }


        [BindProperty] //(Endjin, 2022)
        public int? TrackClaimId { get; set; }

        public LectureModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            LecturerId = 1; 

            if (LecturerId == 0)
                return RedirectToPage("/Login");

            LoadMonthlyAnalytics();
            LoadClaimsHistory();

            return Page();
        }

        public IActionResult OnPostTrackClaim()
        {
            if (TrackClaimId.HasValue)
            {
                var claim = _dbContext.Claims.FirstOrDefault(c => c.ClaimId == TrackClaimId.Value && c.LecturerId == LecturerId);

                if (claim != null)
                {
                    TrackedClaimDetails = $"Claim ID: {claim.ClaimId}\n" +
                                          $"Status: {claim.Status}\n" +
                                          $"Date: {claim.SubmissionDate:yyyy-MM-dd}\n" +
                                          $"Details: {claim.Notes}";
                }
                else
                {
                    TrackedClaimDetails = "Claim not found.";
                }
            }
            else
            {
                TrackedClaimDetails = "Please enter a valid Claim ID.";
            }

            LoadMonthlyAnalytics();
            LoadClaimsHistory();

            return Page();
        }

        private void LoadMonthlyAnalytics()
        {
            try
            {
                var currentMonthClaims = _dbContext.Claims
                    .Where(c => c.LecturerId == LecturerId)
                    .ToList();

                TotalClaims = currentMonthClaims.Count;
                ApprovedClaims = currentMonthClaims.Count(c => c.Status == "Approve");
            }
            catch (Exception ex)
            {
                TrackedClaimDetails = $"An error occurred while loading monthly analytics: {ex.Message}";
            }
        }
        //(C# Corner, 2023)

        private void LoadClaimsHistory()
        {
            try
            {
                ClaimsHistory = _dbContext.Claims
                    .Where(c => c.LecturerId == LecturerId)
                    .Select(c => new ClaimHistory
                    {
                        ClaimId = c.ClaimId,
                        Status = c.Status,
                        SubmissionDate = c.SubmissionDate,
                        Notes = c.Notes
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                TrackedClaimDetails = $"An error occurred while loading claim history: {ex.Message}";
            }
        }
    }
}
/*Reference List:
 

MicroSoftLearn. 2024. Model Binding in ASP.NET Core, 27 September 2024. [Online]. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-9.0 [10 November 2024].  

Endjin.2022. Model Binding in ASP.NET Core using Razor Pages, 18 January 2022. [Online]. Available at: https://endjin.com/blog/2022/01/model-binding-in-asp-net-core-using-razor-pages[10 November 2024].  

LearnRazorPages. [s.a.]. Model Binding in ASP.NET Core using Razor Pages, [s.a.]. [Online]. Available at: https://www.learnrazorpages.com/razor-pages/forms[10 November 2024].  

LearnRazorPages. [s.a]. Working With Forms, [s.a.]. [Online]. Available at: https://www.learnrazorpages.com/razor-pages/tutorial/bakery7/forms[ [10 November 2024] 

Dev.to.2023. Bootstrap 5 ASP.NET Core, 1 April 2023. [Online]. Available at: https://dev.to/karenpayneoregon/bootstrap-5-and-razor-pages-4521 [10 November 2024]. 

C# Corner.2023. Razor In ASP.NET Core, 12 April 2023. [Online]. Available at: https://www.c-sharpcorner.com/blogs/razor-in-asp-net-core [10 November 2024].  

MicroSoftLearn.2024. Entity Framework Core, 12 November 2024. [Online]. Available at:https://learn.microsoft.com/en-us/ef/core/ [15 November 2024].  

LearnRazorPages. [s.a.]. Dependency Injection in Razor Pages, [s.a.]. [Online]. Available at: https://www.learnrazorpages.com/advanced/dependency-injection[15 November 2024].  

Twilio.2020. Adding Asynchronous Processing to ASP.NET Core 3.1 Razor Pages Applications Built With the MVVM Design Pattern, 12 July2020. [Online]. Available at: https://www.twilio.com/en-us/blog/asynchronous-mvvm-asp-dot-net-core-razor-pages [15 November 2024].  

 

LearnRazorPages. [s.a.]. Razor Pages Routing, [s.a.]. [Online]. Available at: https://www.learnrazorpages.com/razor-pages/routing[15 November 2024].  

 

MicroSoftLearn.2024. Handle errors in ASP.NET Core, 22 September 2024. [Online]. Available at: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-9.0[15 November 2024].  

 

Dev.to. 2024. ASP.NET Core + Razor + HTMX + Chart.js, 11 April 2024. [Online]. Available at: https://endjin.com/blog/2024/04/aspnet-core-razor-htmx-chartjs[15 November 2024].  

LearnRazorPages. [s.a.]. State Management In Razor Pages, [s.a.]. [Online]. Available at:https://www.learnrazorpages.com/razor-pages/state-management[15 November 2024].  

 

Khalidabuhakmeh. 2023. HTMX, ASP.NET Core, and Bootstrap Modals, 11 July 2023. [Online]. Available at: https://khalidabuhakmeh.com/htmx-aspnet-core-and-bootstrap-modals [ 19 November 2024].  

 

LearnRazorPages. [s.a.]. Validating User Input in Razor Pages, [s.a.]. [Online]. Available at: https://www.learnrazorpages.com/razor-pages/validation [Accessed 20 November 2024]. 

 








*/
