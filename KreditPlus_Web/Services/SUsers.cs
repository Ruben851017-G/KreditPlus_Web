using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;

namespace KreditPlus_Web.Services
{
    public class SUsers: BaseService, IUsers
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        public SUsers(IHttpClientFactory clientFactory, IConfiguration config, ILogger<SUserType> logger) : base(clientFactory)
        {
            _configuration = config;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public Task<T> CreateAsync<T>(string token, Users model)
        {
            return PostAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Users",
                Data = model,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Users",
                Token = token
            });
        }

        public async Task<APIResponse> UpdateAsync(string token, Users model)
        {
            return await PutAsync(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Users/" + model.UserId,
                Data = model,
                Token = token
            });
        }

        public async Task<APIResponse> GetListUserByIdAsync(string token, int id)
        {
            return await GetListUserByIdAsync(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Users/" + id,
                Token = token
            });
        }

        public async Task<APIResponse> DeleteAsync(string token, int id)
        {
            return await DeleteAsync(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Users/" + id,
                Token = token
            });
        }
    }
}
