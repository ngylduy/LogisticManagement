using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly LogisticDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(LogisticDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels.FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }
            Parcel = parcel;


            ViewData["AddessList"] = _context.Addresses.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Street + ", " + c.City + ", " + c.Country
            }).ToList();

            var user = await _userManager.GetUserAsync(User);

            ViewData["UserList"] = _context.Users.Where(u => u.Id != user.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToList();

            ViewData["CurentUser"] = user.Id;

            return Page();
        }

        [BindProperty]
        public BusinessObject.Models.Parcel Parcel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Parcel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelExists(Parcel.Id))
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
}
