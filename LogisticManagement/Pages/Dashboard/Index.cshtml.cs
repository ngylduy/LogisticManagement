using Data_Access.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticManagement.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly Data_Access.Models.LogisticDatabaseContext _context;

        public IndexModel(Data_Access.Models.LogisticDatabaseContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users.ToListAsync();
            }
        }
    }
}
