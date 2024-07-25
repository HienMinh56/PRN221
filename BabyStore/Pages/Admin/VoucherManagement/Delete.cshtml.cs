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
    public class DeleteModel : PageModel
    {
        private readonly IVoucherService _voucher;

        public DeleteModel(IVoucherService voucher)
        {
            _voucher = voucher;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var voucher = _voucher.GetVoucher(id);
                if (voucher != null)
                {
                    await _voucher.DeleteVoucher(id);
                }

                return RedirectToPage("./Voucher", new
                {
                    message = "Delete Successfull",
                    messageType = "success"
                });
            } catch (Exception ex)
            {
                return RedirectToPage("./Voucher", new
                {
                    message = "Delete failed",
                    messageType = "error"
                });
            }            
        }
    }
}
