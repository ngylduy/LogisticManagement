using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel
{
    [Authorize]
    public class ParcelHistoryModel : PageModel
    {
        private readonly LogisticDbContext _context;

        public ParcelHistoryModel(LogisticDbContext context)
        {
            _context = context;
        }

        public BusinessObject.Models.Parcel Parcel { get; set; } = default!;
        public IList<ParcelHistory> History { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (_context.Parcels == null || _context.ParcelHistories == null || id == null)
            {
                return NotFound();
            }

            Parcel = await _context.Parcels.FirstOrDefaultAsync(m => m.Id == id);
            History = await _context.ParcelHistories.Include(p => p.Address).Where(p => p.ParcelId == id).ToListAsync();

            return Page();
        }
    }
}
