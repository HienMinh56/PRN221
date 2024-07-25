using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;

namespace BabyStore.Pages.Admin.UserManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
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

        public async Task<IActionResult> OnPostAsync(String? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = _userService.GetUserById(id);
                if (user != null)
                {
                    _userService.Remove(id);
                }

                TempData["message"] = "Delete User Successful";
                TempData["messageType"] = "success";

                return RedirectToPage("./User");
            } catch (Exception ex)
            {
                TempData["message"] = "Delete User Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("./User");
            }            
        }
    }
}