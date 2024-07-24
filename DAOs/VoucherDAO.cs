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
            try
            {
                // Check if the voucher code already exists
                var existingVoucher = _dbprn221Context.Vouchers
                    .Any(v => v.VoucherCode == voucher.VoucherCode);

                if (existingVoucher)
                {
                    throw new Exception("Voucher code already exists.");
                }

                // Get the current date
                var currentDate = DateOnly.FromDateTime(DateTime.Now);

                // Determine the status based on the current date
                if (voucher.StartDate <= currentDate && voucher.EndDate >= currentDate)
                {
                    voucher.Status = "Active";
                }
                else
                {
                    voucher.Status = "Inactive";
                }

                voucher.CreatedDate = DateTime.Now;
                _dbprn221Context.Vouchers.Add(voucher);
                await _dbprn221Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateVoucher(Voucher voucher)
        {
            try
            {
                // Retrieve the voucher from the database
                var existingVoucher = _dbprn221Context.Vouchers
                    .SingleOrDefault(v => v.Id == voucher.Id);

                if (existingVoucher != null)
                {
                    // Check if another voucher with the same code exists
                    var duplicateVoucher = _dbprn221Context.Vouchers
                        .SingleOrDefault(v => v.VoucherCode == voucher.VoucherCode && v.Id != voucher.Id);

                    if (duplicateVoucher != null)
                    {
                        throw new Exception("Voucher with the same VoucherCode already exists.");
                    }

                    // Update the voucher details
                    existingVoucher.VoucherCode = voucher.VoucherCode;
                    existingVoucher.Description = voucher.Description;
                    existingVoucher.Discount = voucher.Discount;
                    existingVoucher.StartDate = voucher.StartDate;
                    existingVoucher.EndDate = voucher.EndDate;
                    existingVoucher.MinimumOrderAmount = voucher.MinimumOrderAmount;
                    existingVoucher.Quantity = voucher.Quantity;

                    // Determine the new status based on the current date
                    var currentDate = DateOnly.FromDateTime(DateTime.Now);
                    if (voucher.StartDate <= currentDate && voucher.EndDate >= currentDate)
                    {
                        existingVoucher.Status = "Active";
                    }
                    else
                    {
                        existingVoucher.Status = "Inactive";
                    }

                    // Save changes to the database
                    await _dbprn221Context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Voucher not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the voucher: {ex.Message}", ex);
            }
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
