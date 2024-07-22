using BOs.Entities;
using BOs.Model.CartModel;
using DAOs;
using Repos;
using Repos.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionService(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _transactionRepo.AddTransaction(transaction);
        }

        public Transaction GetTransaction(string transactionId)
        {
            return _transactionRepo.GetTransaction(transactionId);
        }

        public List<Transaction> GetTransactions()
        {
            return _transactionRepo.GetTransactions();
        }

        public async Task UpdateTransactionStatus(string transactionId, int status)
        {
            await _transactionRepo.UpdateTransactionStatus(transactionId, status);
        }

        public async Task<string> GenerateTransactionId()
        {
            return await _transactionRepo.GenerateTransactionId();
        }
    }
}
