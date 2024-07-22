using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetVouchers();
        List<Voucher> GetVouchersActive();
        Voucher GetVoucher(int VoucherId);
        Task AddVoucher(Voucher voucher);
        Task UpdateVoucher(Voucher voucher);
        Task DeleteVoucher(int voucherId);
    }
}
