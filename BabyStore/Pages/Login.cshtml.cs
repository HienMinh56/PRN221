using BOs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Services;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BabyStore.Pages
{
    public class AuthenModel : PageModel
    {
        private readonly IUserService _userService;

        public AuthenModel(IUserService userService)
        {
            _userService = userService;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            public string FullName { get; set; }

            public string Username { get; set; }

            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            public string Phone { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Address { get; set; }
        }

        [BindProperty]
        public RegisterModel registerModel { get; set; }

        [BindProperty]
        public LoginModel loginModel { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            var user = _userService.Login(loginModel.Username, loginModel.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("username", user.FullName);
                HttpContext.Session.SetString("id", user.UserId);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetInt32("role", user.Role);
                HttpContext.Session.SetString("address", user.Address);
                if (user.Status == 0)
                {
                    ViewData["error"] = "Your account has been blocked";
                    return Page();
                }
                else
                {
                    if (user.Role == 1)
                    {
                        return RedirectToPage("/Admin/Dashboard");
                    }
                    return RedirectToPage("/UserMenu/ProductsMenu");
                }
            }
            else
            {
                ViewData["error"] = "Wrong username or password";
                return Page();
            }
        }

        public IActionResult OnPostRegister()
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);

                var user = new User
                {
                    FullName = registerModel.FullName,
                    UserName = registerModel.Username,
                    Email = registerModel.Email,
                    Phone = registerModel.Phone,
                    Password = hashedPassword,
                    Address = registerModel.Address,
                    Role = 2,
                    Status = 1
                };

                _userService.AddUser(user);

                TempData["message"] = "Sign Up Successful";
                TempData["messageType"] = "success";

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                TempData["message"] = "Sign Up Failed";
                TempData["messageType"] = "error";

                return RedirectToPage("/Login");
            }
        }
    }
}