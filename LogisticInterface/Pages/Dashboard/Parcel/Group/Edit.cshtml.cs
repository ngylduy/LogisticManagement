using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel.Group;

public class EditModel : PageModel
{
    private readonly LogisticDbContext _context;

    public EditModel(LogisticDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null || _context.Addresses == null)
        {
            return NotFound();
        }

        var parcelGroup = await _context.ParcelGroups.FirstOrDefaultAsync(m => m.Id == id);

        if (parcelGroup == null)
        {
            return NotFound();
        }

        ParcelGroups = parcelGroup;

        return Page();
    }

    [BindProperty]
    public ParcelGroups ParcelGroups { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(ParcelGroups).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParcelExists(ParcelGroups.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool ParcelExists(string id)
    {
        return (_context.Parcels?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
