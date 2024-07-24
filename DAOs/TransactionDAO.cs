using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class TransactionDAO
    {
        private readonly Dbprn221Context _context;
        private static TransactionDAO instance = null;

        public static TransactionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransactionDAO();
                }
                return instance;
            }
        }

        public TransactionDAO()
        {
            _context = new Dbprn221Context();
        }

        public List<Transaction> GetTransactions()
        {
            return _context.Transactions.Include(u => u.User).ToList();
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public Transaction GetTransaction(string transactionId)
        {
            return _context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }

        public async Task UpdateTransactionStatus(string transactionId, int status)
        {
            var transaction = GetTransaction(transactionId);
            if (transaction != null)
            {
                transaction.Status = status;
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateTransactionId()
        {
            var lastTransaction = await _context.Transactions.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (lastTransaction == null)
            {
                return "TRANS0001";
            }

            string lastTransactionId = lastTransaction.TransactionId;
            int nextIdNumber = int.Parse(lastTransactionId.Substring(5)) + 1;
            return "TRANS" + nextIdNumber.ToString("D4");
        }
    }
}
