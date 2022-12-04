using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IOrderStatus
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> CreateAsync<T>(string token, OrderStatus model);
        Task<APIResponse> GetListOrderStatusByIdAsync(string token, int id);
        Task<APIResponse> UpdateAsync(string token, OrderStatus model);
        Task<APIResponse> DeleteAsync(string token, int id);
    }
}
