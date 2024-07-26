using BOs;
using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class VoucherDAO
    {
        private readonly Dbprn221Context _dbprn221Context;
        private static VoucherDAO instance = null;

        public static VoucherDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VoucherDAO();
                }
                return instance;
            }
        }

        public VoucherDAO()
        {
            _dbprn221Context = new Dbprn221Context();
        }

        public  List<Voucher> GetVouchers()
        {
            return _dbprn221Context.Vouchers.OrderByDescending(v => v.Id).ToList();
        }
        public Voucher GetVoucherById(int VoucherId)
        {
            return _dbprn221Context.Vouchers.OrderByDescending(v => v.Id).SingleOrDefault(v => v.Id==VoucherId);
        }
        public async Task AddVoucher(Voucher voucher)
        {
            _dbprn221Context.Vouchers.Add(voucher);
            await _dbprn221Context.SaveChangesAsync();
        }

        public async Task UpdateVoucher(Voucher voucher)
        {
            _dbprn221Context.Vouchers.Update(voucher);
            await _dbprn221Context.SaveChangesAsync();
        }

        public async Task DeleteVoucher(int VoucherId)
        {
            try
            {
                var voucher = GetVoucherById(VoucherId);
                if (voucher != null)
                {
                    voucher.Status = "Inactive";
                }
                _dbprn221Context.Vouchers.Update(voucher);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
