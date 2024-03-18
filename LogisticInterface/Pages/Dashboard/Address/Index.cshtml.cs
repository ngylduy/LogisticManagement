using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Address
{
    public class IndexModel : PageModel
    {

        private readonly LogisticDbContext _context;

        public IndexModel(LogisticDbContext context)
        {
            _context = context;
        }

        public IList<BusinessObject.Models.Address> Address { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Address = await _context.Addresses.ToListAsync();

            return Page();
        }
    }
}
