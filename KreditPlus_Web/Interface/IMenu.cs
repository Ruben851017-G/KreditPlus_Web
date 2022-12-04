using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IMenu
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> CreateAsync<T>(string token, Menu model);
        Task<APIResponse> GetListByIdAsync(string token, int id);
        Task<APIResponse> UpdateAsync(string token, Menu model);
        Task<APIResponse> DeleteAsync(string token, int id);
    }
}
