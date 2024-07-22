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
        private readonly Dbprn221Context _dbprn221Context;
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
            _dbprn221Context = new Dbprn221Context();
        }

        public List<Transaction> GetTransactions()
        {
            return _dbprn221Context.Transactions.ToList();
        }

        public Transaction GetTransaction(string TransactionId)
        {
            return _dbprn221Context.Transactions.FirstOrDefault(t => t.TransactionId == TransactionId);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _dbprn221Context.Transactions.Add(transaction);
            await _dbprn221Context.SaveChangesAsync();
        }

        public async Task UpdateTransactionStatus(string transactionId, int status)
        {
            var transaction = await _dbprn221Context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);
            if (transaction != null)
            {
                transaction.Status = status;
                await _dbprn221Context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateTransactionId()
        {
            var lastTransaction = await _dbprn221Context.Transactions.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            if (lastTransaction == null)
            {
                return "TRANS0001";
            }
            var lastIdNumber = int.Parse(lastTransaction.TransactionId.Substring(5));
            return $"TRANS{(lastIdNumber + 1).ToString("D4")}";
        }
    }
}
