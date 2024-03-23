using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel.Group;

[Authorize(Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly BusinessObject.Models.LogisticDbContext _context;

    public DeleteModel(BusinessObject.Models.LogisticDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ParcelGroups ParcelGroups { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null || _context.ParcelGroups == null)
        {
            return NotFound();
        }

        var group = await _context.ParcelGroups.FirstOrDefaultAsync(m => m.Id == id);

        if (group == null)
        {
            return NotFound();
        }
        else
        {
            ParcelGroups = group;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? id)
    {
        if (id == null || _context.ParcelGroups == null)
        {
            return NotFound();
        }
        var group = await _context.ParcelGroups
                    .Include(p => p.ParcelGroupItems)
                    .FirstOrDefaultAsync(p => p.Id == id);

        if (group != null)
        {
            ParcelGroups = group;
            _context.ParcelGroups.Remove(group);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
