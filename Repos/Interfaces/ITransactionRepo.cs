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
        Task AddTransaction(Transaction transaction);
        Transaction GetTransaction(string transactionId);
        Task UpdateTransactionStatus(string transactionId, int status);
        Task<string> GenerateTransactionId();
    }
}
