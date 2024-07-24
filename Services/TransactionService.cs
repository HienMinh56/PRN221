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

        public async Task<Transaction> CreateTransaction(string userId, int amount, string orderId)
        {
            var transactionId = await _transactionRepo.GenerateTransactionId();
            var transaction = new Transaction
            {
                TransactionId = transactionId,
                UserId = userId,
                Amount = amount,
                Status = 2, // Pending
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                OrderId = orderId
            };
            await _transactionRepo.AddTransaction(transaction);
            return transaction;
        }


        public async Task AddTransaction(Transaction transaction)
        {
            await _transactionRepo.AddTransaction(transaction);
        }

        public Transaction GetTransactionById(string transactionId)
        {
            return _transactionRepo.GetTransactionById(transactionId);
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
