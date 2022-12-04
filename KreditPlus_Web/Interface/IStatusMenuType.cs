namespace KreditPlus_Web.Interface
{
    public interface IStatusMenuType
    {
        Task<T> GetAllAsync<T>(string token);
    }
}
