using KreditPlus_Web.Models;

namespace KreditPlus_Web.Interface
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
