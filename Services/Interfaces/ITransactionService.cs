using BOs.Entities;
using BOs.Model.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransactionService
    {
        Task AddTransaction(Transaction transaction);
        Transaction GetTransaction(string transactionId);
        List<Transaction> GetTransactions();
        Task UpdateTransactionStatus(string transactionId, int status);
        Task<string> GenerateTransactionId();
    }
}
