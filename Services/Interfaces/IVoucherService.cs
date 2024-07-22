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
        Task AddVoucher(Voucher voucher);
        Task UpdateVoucher(Voucher voucher);
        Task DeleteVoucher(int voucherId);
    }
}
