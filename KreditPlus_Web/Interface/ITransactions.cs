using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface ITransactions
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetByDateAsync<T>(string token,Transactions model);
    }
}
