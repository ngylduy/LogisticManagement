using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.LogisticDbContext _context;

        public DeleteModel(BusinessObject.Models.LogisticDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessObject.Models.Parcel Parcel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Parcels == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels.FirstOrDefaultAsync(m => m.Id == id);

            if (parcel == null)
            {
                return NotFound();
            }
            else
            {
                Parcel = parcel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Parcels == null)
            {
                return NotFound();
            }
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel != null)
            {
                Parcel = parcel;
                _context.Parcels.Remove(Parcel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
