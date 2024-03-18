using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LogisticInterface.Areas.Admin.Pages.Role
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, LogisticDbContext context) : base(roleManager, context)
        {
        }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Phải nhập tên role")]
            [Display(Name = "Tên của Role")]
            [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
            public string? Name { set; get; }

        }

        public IdentityRole Role { set; get; }

        [BindProperty]
        public InputModel Input { set; get; }


        public async Task<IActionResult> OnGet(string roleid)
        {
            if (roleid == null)
            {
                StatusMessage = "Error: Không có thông tin về Role";
                return Page();
            }

            Role = await _roleManager.FindByIdAsync(roleid);

            if (Role != null)
            {
                Input = new InputModel
                {
                    Name = Role.Name
                };
                return Page();
            }
            return NotFound("Role canot found.");
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (roleid == null)
            {
                StatusMessage = "Error: Không có thông tin về Role";
                return Page();
            }
            Role = await _roleManager.FindByIdAsync(roleid);
            if (Role == null)
            {
                StatusMessage = "Error: Không có thông tin về Role";
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(Role);
            if (result.Succeeded)
            {
                StatusMessage = $"Role đã được cập nhật: {Input.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e.Description));
            }

            return Page();
        }

    }
}
