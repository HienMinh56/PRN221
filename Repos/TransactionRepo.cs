using DAOs;
using BOs.Entities;
using Repos.Interfaces;

namespace Repos
{
    public class TransactionRepo : ITransactionRepo
    {

        private readonly TransactionDAO _transactionDAO= null;
        public TransactionRepo()
        {
            if (_transactionDAO == null)
            {
                _transactionDAO = new TransactionDAO();
            }
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await TransactionDAO.Instance.AddTransaction(transaction);
        }

        public Transaction GetTransaction(string TransactionId)
        {
            return TransactionDAO.Instance.GetTransaction(TransactionId);
        }

        public List<Transaction> GetTransactions()
        {
            return TransactionDAO.Instance.GetTransactions();
        }
    }
}
