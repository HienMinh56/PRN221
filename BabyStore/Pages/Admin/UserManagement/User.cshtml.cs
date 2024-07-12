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
using Services;

namespace BabyStore.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private const int PageSize = 10;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> User { get;set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        [BindProperty] public string? SearchBy { get; set; }
        [BindProperty] public string? Keyword { get; set; }

        public IActionResult OnGet(int? pageIndex)
        {
            var UserList = _userService.GetUsers();
            PageIndex = pageIndex ?? 1;

            // Paginate the list
            var count = UserList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = UserList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            User = items;
            return Page();
        }

        public async Task OnPost(int? pageIndex)
        {
            if (Keyword == null)
            {
                OnGet(pageIndex);
            }
            else
            {
                if (SearchBy.Equals("UserId"))
                {
                    User = _userService.GetUsers().Where(a => a.UserId.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                else if (SearchBy.Equals("Email"))
                {
                    User = _userService.GetUsers().Where(a => a.Email.ToUpper().Contains(Keyword.Trim().ToUpper())).ToList();
                }
                PageIndex = 1;
                TotalPages = 1;
            }
        }
    }
}