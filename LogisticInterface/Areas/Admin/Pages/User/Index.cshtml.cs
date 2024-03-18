using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogisticInterface.Areas.Admin.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public class UserAndRole : AppUser
        {
            public string RoleName { get; set; }
        }

        public List<UserAndRole> Users { set; get; }


        public async Task OnGet()
        {
            Users = await _userManager.Users.Select(u => new UserAndRole
            {
                Id = u.Id,
                UserName = u.UserName,
                FullName = u.FullName
            }).ToListAsync();

            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleName = string.Join(",", roles);
            }
        }

        public void OnPost() => RedirectToPage();
    }
}
