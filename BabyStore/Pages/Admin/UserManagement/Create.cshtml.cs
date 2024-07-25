using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;
using BOs.Entities;
using Services.Interfaces;

namespace BabyStore.Pages.Admin.UserManagement
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userAdd = HttpContext.Session.GetString("username");
                User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);
                User.CreatedBy = userAdd;
                User.CreatedDate = DateTime.Now;
                _userService.Add(User);

                return RedirectToPage("./User", new
                {
                    message = "Add Successfull",
                    messageType = "success"
                });
            } catch (Exception ex)
            {
                return RedirectToPage("./User", new
                {
                    message = "Add failed",
                    messageType = "error"
                });
            }            
        }
    }
}
