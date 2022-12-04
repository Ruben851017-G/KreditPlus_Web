using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IUsers
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> CreateAsync<T>(string token, Users model);
        Task<APIResponse> UpdateAsync(string token, Users model);
        Task<APIResponse> GetListUserByIdAsync(string token, int id);
        Task<APIResponse> DeleteAsync(string token, int id);
    }
}
