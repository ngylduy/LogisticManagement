using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel
{
    public class ReciveParcelModel : PageModel
    {
        private readonly LogisticDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReciveParcelModel(LogisticDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<BusinessObject.Models.Parcel> Parcel { get; set; } = default!;
        public IList<BusinessObject.Models.Address> Addresses { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string addressId)
        {
            if (_context.Parcels == null)
            {
                return NotFound();
            }

            ViewData["AddessList"] = _context.Addresses.Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Street + ", " + c.City + ", " + c.Country
            }).ToList();

            string userRole = _userManager.GetRolesAsync(await _userManager.GetUserAsync(User)).Result.FirstOrDefault();
            string userId = _userManager.GetUserId(User);

            if (addressId == null)
            {
                Parcel = await _context.Parcels.Where(p => p.ReceiverUserId == userId)
                                            .Include(p => p.SenderUser)
                                            .Include(p => p.ReceiverUser)
                                            .Include(p => p.PickupAddress)
                                            .Include(p => p.DeliveryAddress)
                                            .ToListAsync();

                Addresses = await _context.Addresses.Where(a => a.deliveryParcel.Count > 0).ToListAsync();
            }
            else
            {
                Parcel = await _context.Parcels.Where(p => p.DeliveryAddressId == addressId)
                                            .Where(p => p.ReceiverUserId == userId)
                                            .Include(p => p.SenderUser)
                                            .Include(p => p.ReceiverUser)
                                            .Include(p => p.PickupAddress)
                                            .Include(p => p.DeliveryAddress)
                                            .ToListAsync();

                Addresses = await _context.Addresses.Where(a => a.Id == addressId).ToListAsync();
            }

            return Page();
        }
    }
}
