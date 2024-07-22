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
    public class DetailsModel : PageModel
    {
        private readonly IVoucherService _voucher;

        public DetailsModel(IVoucherService voucher)
        {
            _voucher = voucher;
        }

        public Voucher Voucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = _voucher.GetVoucher(id);
            if (voucher == null)
            {
                return NotFound();
            }
            else
            {
                Voucher = voucher;
            }
            return Page();
        }
    }
}
