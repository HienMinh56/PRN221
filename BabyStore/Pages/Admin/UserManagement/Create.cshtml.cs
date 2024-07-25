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
                await _userService.AddUser(User);
                _userService.AddUser(User);

                TempData["message"] = "Add User Successful";
                TempData["messageType"] = "success";

                return RedirectToPage("./User");
            } catch (Exception ex)
            {
                TempData["message"] = "Add User Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("./User");
            }            
        }
    }
}