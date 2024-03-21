using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogisticInterface.Pages.Dashboard.Parcel
{
    public class CreateModel : PageModel
    {
        private readonly LogisticDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(LogisticDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
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
            var counter = _context.ParcelIdCounters.FirstOrDefault();
            if (counter == null)
            {
                counter = new ParcelIdCounter { LastNumberUsed = 1 };
                _context.ParcelIdCounters.Add(counter);
            }
            else
            {
                counter.LastNumberUsed++;
            }

            Parcel.Id = "DUYDT" + counter.LastNumberUsed.ToString("D6"); ;

            if (!ModelState.IsValid || _context.Parcels == null || Parcel == null)
            {
                return Page();
            }
            _context.Parcels.Add(Parcel);

            var history = new ParcelHistory
            {
                ParcelId = Parcel.Id,
                Status = "Created",
                Date = DateTime.Now,
                AddressId = Parcel.PickupAddressId
            };

            _context.ParcelHistories.Add(history);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
