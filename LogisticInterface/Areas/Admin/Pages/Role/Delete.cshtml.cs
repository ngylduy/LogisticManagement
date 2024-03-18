using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogisticInterface.Areas.Admin.Pages.Role
{
    public class DeleteModel : RolePageModel
    {

        public DeleteModel(RoleManager<IdentityRole> roleManager, LogisticDbContext context) : base(roleManager, context)
        {
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IdentityRole Role { set; get; }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if (roleid == null)
            {
                return NotFound("Không có thông tin về Role");
            }
            Role = await _roleManager.FindByIdAsync(roleid);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string roleid)
        {
            if (roleid == null)
            {
                return NotFound("Không có thông tin về Role");
            }
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không thấy role cần xóa");
            }


            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = "Đã xóa " + role.Name;
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            return Page();
        }
    }
}
