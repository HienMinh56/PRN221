using BOs.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Interfaces
{
    public interface ITransactionRepo
    {
        public List<Transaction> GetTransactions();

        public Transaction GetTransaction(string TransactionId);

        public Task AddTransaction(Transaction transaction);
    }
}
