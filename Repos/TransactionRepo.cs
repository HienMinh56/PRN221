using DAOs;
using BOs.Entities;
using Repos.Interfaces;

namespace Repos
{
    public class TransactionRepo : ITransactionRepo
    {
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
