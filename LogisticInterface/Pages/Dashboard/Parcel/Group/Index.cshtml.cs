using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Pages.Dashboard.Parcel.Group
{
    public class IndexModel : PageModel
    {

        private readonly LogisticDbContext _context;

        public IndexModel(LogisticDbContext context)
        {
            _context = context;
        }

        public IList<ParcelGroups> Groups { get; set; } = default!;
        public IList<BusinessObject.Models.Parcel> Parcels { get; set; } = default!;
        public IList<BusinessObject.Models.ParcelGroupItems> ParcelGroupItems { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Groups = await _context.ParcelGroups
                .Include(pg => pg.ParcelGroupItems)
                    .ThenInclude(pgi => pgi.ParcelItem)
                        .ThenInclude(p => p.SenderUser)
                .Include(pg => pg.ParcelGroupItems)
                    .ThenInclude(pgi => pgi.ParcelItem)
                        .ThenInclude(p => p.ReceiverUser)
                .Include(pg => pg.ParcelGroupItems)
                    .ThenInclude(pgi => pgi.ParcelItem)
                        .ThenInclude(p => p.PickupAddress)
                .Include(pg => pg.ParcelGroupItems)
                    .ThenInclude(pgi => pgi.ParcelItem)
                        .ThenInclude(p => p.DeliveryAddress)
                .ToListAsync();
            return Page();
        }
    }
}
