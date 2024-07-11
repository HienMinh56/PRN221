using BOs.Entities;
using DAOs;
using Repos;
using Services.Interfaces;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepo transactionRepo;

        public TransactionService()
        {
            transactionRepo = new TransactionRepo();
        }

        public Transaction GetTransaction(string TransactionId)
        {
            return transactionRepo.GetTransaction(TransactionId);
        }

        public List<Transaction> GetTransactions()
        {
            return transactionRepo.GetTransactions();
        }
    }
}
