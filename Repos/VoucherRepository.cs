using BOs.Entities;
using DAOs;
using Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly VoucherDAO _voucherDAO = null;
        public VoucherRepository()
        {
            if (_voucherDAO == null)
            {
                _voucherDAO = new VoucherDAO();
            }
        }
        public async Task AddVoucher(Voucher voucher)
        {
             _voucherDAO.AddVoucher(voucher);
        }

        public async Task DeleteVoucher(int voucherId)
        {
            _voucherDAO.DeleteVoucher(voucherId);
        }

        public Voucher GetVoucher(int VoucherId)
        {
            return _voucherDAO.GetVoucher(VoucherId);
        }

        public List<Voucher> GetVouchers()
        {
            return _voucherDAO.GetVouchers();
        }

        public async Task UpdateVoucher(Voucher voucher)
        {
           _voucherDAO.UpdateVoucher(voucher);
        }
    }
}
