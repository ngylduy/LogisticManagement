// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LogisticInterface.Areas.Admin.Pages.User;

[Authorize(Roles = "Admin")]
public class SetPasswordModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public SetPasswordModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [BindProperty]
    public InputModel Input { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public class InputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public AppUser user { get; set; }

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

        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _userManager.RemovePasswordAsync(user);

        var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            foreach (var error in addPasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        StatusMessage = $"Password has been set for {user.UserName}";

        return RedirectToPage();
    }
}
