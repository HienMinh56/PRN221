using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BabyStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            var user = userService.Login(Username, Password);
            if (user != null)
            {
                HttpContext.Session.SetString("username", user.FullName);
                HttpContext.Session.SetInt32("id", user.Id);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetInt32("role", user.Role);
                if(user.Role == 1)
                {
                    // Nếu là admin
                    return RedirectToPage("/Admin/Dashboard");
                }
                // Đăng nhập thành công, chuyển hướng đến trang khác hoặc lưu thông tin đăng nhập
                return RedirectToPage("/Admin/ProductManagement/Index");
            }
            else
            {
                ViewData["error"] = "Wrong username or password";
                return Page();
            }
        }
    }
}
