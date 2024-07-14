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

        public Transaction GetTransaction(string TransationId)
        {
            return _dbprn221Context.Transactions.Find(TransationId);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _dbprn221Context.Transactions.Add(transaction);
            await _dbprn221Context.SaveChangesAsync();
        }
    }
}
