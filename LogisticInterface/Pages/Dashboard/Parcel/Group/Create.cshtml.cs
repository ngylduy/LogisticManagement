using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogisticInterface.Pages.Dashboard.Parcel.Group;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{

    private readonly LogisticDbContext _context;

    public CreateModel(LogisticDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ParcelGroups ParcelGroup { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.ParcelGroups.Add(ParcelGroup);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
