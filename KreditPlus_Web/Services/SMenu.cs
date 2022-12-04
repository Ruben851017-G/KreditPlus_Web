using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;

namespace KreditPlus_Web.Services
{
    public class SMenu: BaseService, IMenu
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public SMenu(IHttpClientFactory clientFactory, IConfiguration config, ILogger<SMenu> logger) : base(clientFactory)
        {
            _configuration = config;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Menu",
                Token = token
            });
        }

        public Task<T> CreateAsync<T>(string token, Menu model)
        {
            return PostAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Menu",
                Data = model,
                Token = token
            });
        }

        public async Task<APIResponse> GetListByIdAsync(string token, int id)
        {
            return await GetListByIdMenu(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Menu/" + id,
                Token = token
            });
        }

        public async Task<APIResponse> UpdateAsync(string token, Menu model)
        {
            return await PutAsync(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Menu/" + model.MenuId,
                Data = model,
                Token = token
            });
        }

        public async Task<APIResponse> DeleteAsync(string token, int id)
        {
            return await DeleteAsync(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Menu/" + id,
                Token = token
            });
        }
    }
}
