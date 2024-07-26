using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Entities;
using Services.Interfaces;

namespace BabyStore.Pages.Admin.VoucherManagement
{
    public class EditModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public EditModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = _voucherService.GetVoucher(id);
            if (voucher == null)
            {
                return NotFound();
            }
            Voucher = voucher;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var id = _voucherService.GetVoucher(Voucher.Id);
                if (id == null)
                {
                    return NotFound();
                }
                await _voucherService.UpdateVoucher(Voucher);
                TempData["message"] = "Update Voucher Successful";
                TempData["messageType"] = "success";
                
                return RedirectToPage("./Voucher");
            }
            catch (Exception ex)
            {
                TempData["message"] = "Update Voucher Failed";
                TempData["messageType"] = "error";
                return RedirectToPage("./Voucher");
            }
        }
    }
}
