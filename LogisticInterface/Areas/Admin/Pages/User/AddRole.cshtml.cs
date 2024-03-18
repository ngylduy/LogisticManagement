// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace LogisticInterface.Areas.Admin.Pages.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }


        public AppUser user { get; set; }

        [BindProperty]
        [DisplayName("Role assign for user")]
        public string[] RoleNames { get; set; }
        public SelectList allRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return NotFound("User not found!");
            }

            user = await _userManager.FindByIdAsync(userid);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userid}'.");
            }

            RoleNames = (await _userManager.GetRolesAsync(user)).ToArray<string>();

            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return NotFound("User not found!");
            }

            user = await _userManager.FindByIdAsync(userid);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userid}'.");
            }

            var OldRoles = (await _userManager.GetRolesAsync(user)).ToArray();

            var deleteRoles = OldRoles.Where(r => !RoleNames.Contains(r));
            var addRoles = RoleNames.Where(r => !OldRoles.Contains(r));

            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);

            var resultDelete = await _userManager.RemoveFromRolesAsync(user, deleteRoles);

            if (!resultDelete.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }

            var resultAdded = await _userManager.AddToRolesAsync(user, addRoles);

            if (!resultAdded.Succeeded)
            {
                resultAdded.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }

            StatusMessage = "Roles has been updated for " + user.UserName;

            return RedirectToPage("./Index");
        }
    }
}
