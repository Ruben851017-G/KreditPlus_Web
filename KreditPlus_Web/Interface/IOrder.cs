using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IOrder
    {
        Task<T> GetAllAsync<T>(string token);
        Task<APIResponse> UpdateAsync(string token, Order model);
    }
}
