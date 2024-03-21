using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Tracking;

[AllowAnonymous]
public class ParcelsModel : PageModel
{
    private readonly LogisticDbContext _context;

    public ParcelsModel(LogisticDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string? ParcelId { get; set; }

    public IList<ParcelHistory> ParcelHistory { get; set; } = default!;

    public async Task<IActionResult> OnGet(string? ParcelId)
    {
        if (ParcelId != null)
        {
            ParcelHistory = await _context.ParcelHistories
                .Include(p => p.Parcel)
                .Include(p => p.Address)
                .Where(p => p.ParcelId == ParcelId)
                .ToListAsync();

            if (ParcelHistory.Count > 0)
            {
                ViewData["DataCheck"] = true;
            }
            else
            {
                ViewData["DataCheck"] = false;
            }
        }

        return Page();
    }
}
