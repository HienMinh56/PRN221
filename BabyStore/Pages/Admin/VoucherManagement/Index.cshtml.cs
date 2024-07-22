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

namespace BabyStore.Pages.Admin.VoucherManagement
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucher;

        public IndexModel(IVoucherService voucher)
        {
            _voucher = voucher;
        }

        public IList<Voucher> Voucher { get;set; } = default!;

        public IActionResult OnGetAsync()
        {
            Voucher =  _voucher.GetVouchers();
            return Page();
        }
    }
}
