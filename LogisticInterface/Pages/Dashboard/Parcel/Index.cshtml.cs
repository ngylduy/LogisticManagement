using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel;

[Authorize]
public class IndexModel : PageModel
{
    private readonly LogisticDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public IndexModel(LogisticDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<BusinessObject.Models.Parcel> Parcel { get; set; } = default!;
    public IList<BusinessObject.Models.Address> Addresses { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string? addressId, string? street, string? city, string? country)
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

        ViewData["StreetList"] = _context.Addresses.Select(c => new SelectListItem
        {
            Value = c.Street,
            Text = c.Street
        }).ToList();

        ViewData["CityList"] = _context.Addresses.Select(c => c.City)
            .Distinct()
            .Select(c => new SelectListItem
            {
                Value = c,
                Text = c
            }).ToList();

        ViewData["CountryList"] = _context.Addresses.Select(c => c.Country)
            .Distinct()
            .Select(c => new SelectListItem
            {
                Value = c,
                Text = c
            }).ToList();

        string userRole = _userManager.GetRolesAsync(await _userManager.GetUserAsync(User)).Result.FirstOrDefault();
        string userId = _userManager.GetUserId(User);

        if (addressId == null)
        {
            if (userRole.Equals("Admin"))
            {
                Parcel = await _context.Parcels
                                        .Include(p => p.SenderUser)
                                        .Include(p => p.ReceiverUser)
                                        .Include(p => p.PickupAddress)
                                        .Include(p => p.DeliveryAddress)
                                        .ToListAsync();
            }
            else
            {
                Parcel = await _context.Parcels.Where(p => p.SenderUserId == userId || p.ReceiverUserId == userId)
                                                               .Include(p => p.SenderUser)
                                                               .Include(p => p.ReceiverUser)
                                                               .Include(p => p.PickupAddress)
                                                               .Include(p => p.DeliveryAddress)
                                                               .ToListAsync();
            }
            Addresses = await _context.Addresses.Where(a => a.deliveryParcel.Count > 0)
                 .ToListAsync();
        }
        else
        {

            if (userRole.Equals("Admin"))
            {
                Parcel = await _context.Parcels.Where(p => p.DeliveryAddressId == addressId)
                .Include(p => p.SenderUser)
                .Include(p => p.ReceiverUser)
                .Include(p => p.PickupAddress)
                .Include(p => p.DeliveryAddress)
                .ToListAsync();
            }
            else
            {
                Parcel = await _context.Parcels.Where(p => p.DeliveryAddressId == addressId)
                .Where(p => p.SenderUserId == userId || p.ReceiverUserId == userId)
                .Include(p => p.SenderUser)
                .Include(p => p.ReceiverUser)
                .Include(p => p.PickupAddress)
                .Include(p => p.DeliveryAddress)
                .ToListAsync();
            }
            Addresses = await _context.Addresses.Where(a => a.Id == addressId).ToListAsync();
        }

        return Page();
    }
}
