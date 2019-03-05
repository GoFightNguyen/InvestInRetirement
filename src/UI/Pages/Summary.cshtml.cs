using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    public class SummaryModel : PageModel
    {
        [BindProperty]
        public decimal InvestmentPercentage { get; set; }
        [BindProperty]
        public decimal AnnualSalary { get; set; }
        public decimal MoniesToInvest { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Percent investmentPercentage = InvestmentPercentage;
            MoniesToInvest = investmentPercentage.Of(AnnualSalary);
            return Page();
        }
    }
}