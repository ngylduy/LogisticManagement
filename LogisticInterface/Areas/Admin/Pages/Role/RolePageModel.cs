using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogisticInterface.Areas.Admin.Pages.Role
{
    public class RolePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly LogisticDbContext _context;
        public RolePageModel(RoleManager<IdentityRole> roleManager, LogisticDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
    }
}
