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

namespace BabyStore.Pages.Admin.TransactionManagement
{
    public class IndexModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private const int PageSize = 8;

        public IndexModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IList<Transaction> Transaction { get; set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        [BindProperty]
        public string? TransactionId { get; set; }
        [BindProperty]
        public string? UserId { get; set; }
        [BindProperty]
        public int? Status { get; set; }

        public IActionResult OnGet(int? pageIndex, string? transactionId, string? userId, int? status)
        {
            TransactionId = transactionId;
            UserId = userId;
            Status = status;

            var TransactionList = _transactionService.GetTransactions();

            if (!string.IsNullOrEmpty(TransactionId))
            {
                TransactionList = TransactionList.Where(a => a.TransactionId.ToUpper().Contains(TransactionId.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(UserId))
            {
                TransactionList = TransactionList.Where(a => a.UserId.ToUpper().Contains(UserId.Trim().ToUpper())).ToList();
            }

            if (Status.HasValue)
            {
                TransactionList = TransactionList.Where(a => a.Status == Status.Value).ToList();
            }

            PageIndex = pageIndex ?? 1;

            // Paginate the filtered list
            var count = TransactionList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = TransactionList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Transaction = items;
            return Page();
        }


        public IActionResult OnPost(int? pageIndex)
        {
            return RedirectToPage(new
            {
                pageIndex,
                transactionId = TransactionId,
                userId = UserId,
                status = Status
            });
        }
    }
}
