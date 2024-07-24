using Repos.Interfaces;
using Repos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;

namespace Services
{
    public class VoucherServcie : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository = null;
        public VoucherServcie()
        {
            if (_voucherRepository == null)
            {
                _voucherRepository = new VoucherRepository();
            }
        }

        public async Task AddVoucher(Voucher voucher)
        {
            await _voucherRepository.AddVoucher(voucher);
        }

        public async Task DeleteVoucher(int voucherId)
        {
            await _voucherRepository.DeleteVoucher(voucherId);
        }

        public Voucher GetVoucher(int VoucherId)
        {
            return _voucherRepository.GetVoucher(VoucherId);
        }

        public Voucher GetVoucherByCode(string voucherCode)
        {
            return _voucherRepository.GetVouchers().Where(v => v.VoucherCode==voucherCode).FirstOrDefault();
        }

        public List<Voucher> GetVouchers()
        {
            return _voucherRepository.GetVouchers();
        }

        public List<Voucher> GetVouchersActive()
        {
            return _voucherRepository.GetVouchersActive();
        }

        public async Task UpdateVoucher(Voucher voucher)
        {
            await _voucherRepository.UpdateVoucher(voucher);
        }

        public async Task UpdateVoucherQuantity(string voucherCode, int quantityUsed)
        {
            var eVoucher = _voucherRepository.GetVouchers().Where(v => v.VoucherCode == voucherCode).FirstOrDefault();
            if (eVoucher != null)
            {
                eVoucher.Quantity = eVoucher.Quantity - quantityUsed;
                await _voucherRepository.UpdateVoucher(eVoucher);
            }
            
        }
    }
}
