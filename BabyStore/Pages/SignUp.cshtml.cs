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
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IUserService _userService;

        public SignUpModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Phone]
        public string Phone { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string Address { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            // Create a user object and set properties
            var user = new User
            {
                FullName = FullName,
                UserName = Username,
                Email = Email,
                Phone = Phone,
                Password = hashedPassword, // Assign hashed password
                Address = Address,
                Role = 2, // Example role assignment
                Status = 1 // Example status assignment
            };

            // Add user to database using your UserService
            _userService.Add(user);

            // Redirect to a success page or another page as needed
            return RedirectToPage("Login");
        }
    }
}
