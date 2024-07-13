using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransactionService
    {
        public List<Transaction> GetTransactions();

        public Transaction GetTransaction(string TransactionId);

        public Task AddTransaction(Transaction transaction);
    }
}
