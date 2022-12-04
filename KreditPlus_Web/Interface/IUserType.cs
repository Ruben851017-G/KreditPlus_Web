using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IUserType
    {
        Task<T> GetAllAsync<T>(string token);
        //Task<T> CreateAsync<T>(string token, UserType model);
        //Task<APIResponse> UpdateAsync(string token, UserType model);
        //Task<T> GetByIdAsync<T>(string token, int id);
        //Task<APIResponse> GetListByIdAsync(string token, int id);
        //Task<APIResponse> DeleteAsync(string token, int id);
    }
}
