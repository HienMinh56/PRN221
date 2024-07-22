using DAOs;
using BOs.Entities;
using Repos.Interfaces;

namespace Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly TransactionDAO _transactionDAO;

        public TransactionRepo()
        {
            _transactionDAO = new TransactionDAO();
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _transactionDAO.AddTransaction(transaction);
        }

        public Transaction GetTransactionById(string transactionId)
        {
            return _transactionDAO.GetTransaction(transactionId);
        }

        public async Task UpdateTransactionStatus(string transactionId, int status)
        {
            await _transactionDAO.UpdateTransactionStatus(transactionId, status);
        }

        public async Task<string> GenerateTransactionId()
        {
            return await _transactionDAO.GenerateTransactionId();
        }

        public List<Transaction> GetTransactions()
        {
            return _transactionDAO.GetTransactions();
        }
    }
}
