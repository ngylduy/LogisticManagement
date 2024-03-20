using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel;

public class EditHistoryModel : PageModel
{
    private readonly LogisticDbContext _context;

    public EditHistoryModel(LogisticDbContext context)
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

        ViewData["AddessList"] = _context.Addresses.Select(c => new SelectListItem
        {
            Value = c.Id,
            Text = c.Street + ", " + c.City + ", " + c.Country
        }).ToList();

        return Page();
    }

    [BindProperty]
    public ParcelHistory ParcelHistory { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.ParcelHistories == null || ParcelHistory == null)
        {
            return Page();
        }

        _context.ParcelHistories.Add(ParcelHistory);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
