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
                // Đăng nhập thành công, chuyển hướng đến trang khác hoặc lưu thông tin đăng nhập
                return RedirectToPage("Error");
            }
            else
            {
                // Đăng nhập thất bại, hiển thị thông báo lỗi
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

    }
}
