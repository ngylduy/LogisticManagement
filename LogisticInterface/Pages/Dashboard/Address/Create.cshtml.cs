using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogisticInterface.Pages.Dashboard.Address
{
    public class CreateModel : PageModel
    {
        private readonly LogisticDbContext _context;

        public CreateModel(LogisticDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public BusinessObject.Models.Address Address { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Addresses == null || Address == null)
            {
                return Page();
            }

            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
