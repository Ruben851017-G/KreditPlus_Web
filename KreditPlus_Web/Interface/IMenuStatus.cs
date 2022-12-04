namespace KreditPlus_Web.Interface
{
    public interface IMenuStatus
    {
        Task<T> GetAllAsync<T>(string token);
    }
}
