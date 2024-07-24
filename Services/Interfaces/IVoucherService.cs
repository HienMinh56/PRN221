using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVoucherService
    {
        List<Voucher> GetVouchers();
        Voucher GetVoucher(int VoucherId);
        Voucher GetVoucherByCode(string voucherCode);  
        List<Voucher> GetVouchersActive();
        Task AddVoucher(Voucher voucher);
        Task UpdateVoucher(Voucher voucher);
        Task UpdateVoucherQuantity(string voucherCode, int quantityUsed);
        Task DeleteVoucher(int voucherId);
    }
}
