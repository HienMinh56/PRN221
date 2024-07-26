using Repos.Interfaces;
using Repos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;
using BOs;

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
            try
            {
                var existingVoucher = GetVouchers().Any(v => v.VoucherCode == voucher.VoucherCode);

                if (existingVoucher)
                {
                    throw new Exception("Voucher code already exists.");
                }
                if(voucher.StartDate > voucher.EndDate)
                {
                    throw new Exception("Start date must be before end date.");
                }
                var currentDate = DateOnly.FromDateTime(DateTime.Now);

                if (voucher.StartDate <= currentDate && voucher.EndDate >= currentDate)
                {
                    voucher.Status = "Active";
                }
                else
                {
                    voucher.Status = "Inactive";
                }

                voucher.CreatedDate = DateTime.Now;
                await _voucherRepository.AddVoucher(voucher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            try
            {
                var existingVoucher = GetVoucherByCode(voucher.VoucherCode);

                if (existingVoucher != null)
                {
                    var duplicateVoucher = GetVouchers().Any(v => v.VoucherCode == voucher.VoucherCode && v.Id != voucher.Id);

                    if (duplicateVoucher)
                    {
                        throw new Exception("Voucher with the same VoucherCode already exists.");
                    }

                    existingVoucher.VoucherCode = voucher.VoucherCode;
                    existingVoucher.Description = voucher.Description;
                    existingVoucher.Discount = voucher.Discount;
                    existingVoucher.StartDate = voucher.StartDate;
                    existingVoucher.EndDate = voucher.EndDate;
                    existingVoucher.MinimumOrderAmount = voucher.MinimumOrderAmount;
                    existingVoucher.Quantity = voucher.Quantity;

                    if (voucher.StartDate > voucher.EndDate)
                    {
                        throw new Exception("Start date must be before end date.");
                    }

                    var currentDate = DateOnly.FromDateTime(DateTime.Now);
                    if (voucher.StartDate <= currentDate && voucher.EndDate >= currentDate)
                    {
                        existingVoucher.Status = "Active";
                    }
                    else
                    {
                        existingVoucher.Status = "Inactive";
                    }

                    await _voucherRepository.UpdateVoucher(existingVoucher);
                }
                else
                {
                    throw new Exception("Voucher not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
