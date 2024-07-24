using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BOs.Entities;
using Services.Interfaces;

namespace BabyStore.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private const int PageSize = 8;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> User { get; set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        [BindProperty]
        public string? UserId { get; set; }
        [BindProperty]
        public string? UserName { get; set; }
        [BindProperty]
        public int? Status { get; set; }
        [BindProperty]
        public int? Role { get; set; }

        public IActionResult OnGet(int? pageIndex, string? userId, string? userName, int? status, int? role)
        {
            UserId = userId;
            UserName = userName;
            Status = status;
            Role = role;

            var UserList = _userService.GetUsers();

            if (!string.IsNullOrEmpty(UserId))
            {
                UserList = UserList.Where(a => a.UserId.ToUpper().Contains(UserId.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                UserList = UserList.Where(a => a.UserName.ToUpper().Contains(UserName.Trim().ToUpper())).ToList();
            }

            if (Status.HasValue)
            {
                UserList = UserList.Where(a => a.Status == Status.Value).ToList();
            }

            if (Role.HasValue)
            {
                UserList = UserList.Where(a => a.Role == Role.Value).ToList();
            }

            PageIndex = pageIndex ?? 1;

            // Paginate the filtered list
            var count = UserList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = UserList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            User = items;
            return Page();
        }

        public IActionResult OnPost(int? pageIndex)
        {
            return RedirectToPage(new
            {
                pageIndex,
                userId = UserId,
                userName = UserName,
                status = Status,
                role = Role
            });
        }
    }
}