using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel.Group
{
    public class AddParcelModel : PageModel
    {
        private readonly LogisticDbContext _context;

        public AddParcelModel(LogisticDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<string> ParcelsId { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            if (_context.ParcelGroupItems == null || _context.Parcels == null)
            {
                return NotFound();
            }

            ViewData["ParcelList"] = _context.Parcels.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name
            }).ToList();

            ParcelsId = await _context.ParcelGroupItems.Where(c => c.ParcelGroupId == id).Select(c => c.ParcelId).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (_context.ParcelGroupItems == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var OldParcel = await _context.ParcelGroupItems.Where(c => c.ParcelGroupId == id).Select(c => c.ParcelId).ToListAsync();

            var deleteParcel = OldParcel.Where(r => !ParcelsId.Contains(r));

            var addParcel = ParcelsId.Where(r => !OldParcel.Contains(r));

            ViewData["ParcelList"] = _context.Parcels.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name
            }).ToList();

            foreach (var parcel in deleteParcel)
            {
                _context.ParcelGroupItems.Remove(new ParcelGroupItems
                {
                    ParcelGroupId = id,
                    ParcelId = parcel
                });
            }

            foreach (var parcel in addParcel)
            {
                _context.ParcelGroupItems.Add(new ParcelGroupItems
                {
                    ParcelGroupId = id,
                    ParcelId = parcel
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
