using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;
using Services;

namespace BabyStore.Pages.Admin.UserManagement
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(String? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, User user)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                await _userService.UpdateUser(id, user);

                return RedirectToPage("./User", new
                {
                    message = "Update Successfull",
                    messageType = "success"
                });
            } catch (Exception ex)
            {
                return RedirectToPage("./User", new
                {
                    message = "Update failed",
                    messageType = "error"
                });
            }            
        }
    }
}
