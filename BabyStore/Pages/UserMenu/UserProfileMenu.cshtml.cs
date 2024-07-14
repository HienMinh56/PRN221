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

namespace BabyStore.Pages.UserMenu
{
    public class UserProfileMenuModel : PageModel
    {
        private readonly IUserService _userService;

        public UserProfileMenuModel(IUserService userService)
        {
            _userService = userService;
        }

        public User User { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = _userService.getUserByid(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
