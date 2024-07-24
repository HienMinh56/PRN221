using BOs.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BabyStore.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;

        public ProfileModel(IUserService userService)
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
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.Update(id, user);

            return Page();
        }
    }
}
