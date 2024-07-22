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
using System.Data;

namespace BabyStore.Pages.Admin.VoucherManagement
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private const int PageSize = 8;

        public IndexModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public IList<Voucher> Voucher { get;set; } = default!;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        [BindProperty]
        public string? Status { get; set; }
        [BindProperty]
        public string? VoucherCode { get; set; }
        [BindProperty]
        public DateOnly? StartDate { get; set; }
        [BindProperty]
        public DateOnly? EndDate { get; set; }

        public IActionResult OnGet(int? pageIndex)
        {
            var VoucherList = _voucherService.GetVouchers();
            PageIndex = pageIndex ?? 1;

            // Paginate the list
            var count = VoucherList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = VoucherList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Voucher = items;
            return Page();
        }

        public IActionResult OnPost(int? pageIndex)
        {
            var VoucherList = _voucherService.GetVouchers();

            if (!string.IsNullOrEmpty(Status))
            {
                VoucherList = VoucherList.Where(a => a.Status.ToUpper().Equals(Status.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(VoucherCode))
            {
                VoucherList = VoucherList.Where(a => a.VoucherCode.ToUpper().Equals(VoucherCode.Trim().ToUpper())).ToList();
            }

            if (StartDate.HasValue && EndDate.HasValue)
            {
                VoucherList = VoucherList.Where(a =>
                    (a.StartDate >= StartDate.Value && a.StartDate <= EndDate.Value) ||
                    (a.EndDate >= StartDate.Value && a.EndDate <= EndDate.Value) ||
                    (a.StartDate <= StartDate.Value && a.EndDate >= EndDate.Value)).ToList();
            }

            PageIndex = pageIndex ?? 1;

            // Paginate the filtered list
            var count = VoucherList.Count();
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = VoucherList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            Voucher = items;
            return Page();
        }
    }
}
