using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;

namespace KreditPlus_Web.Services
{
    public class SOrderStatus: BaseService, IOrderStatus
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public SOrderStatus(IHttpClientFactory clientFactory, IConfiguration config, ILogger<SOrderStatus> logger) : base(clientFactory)
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
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "OrderStatus",
                Token = token
            });
        }

        public Task<T> CreateAsync<T>(string token, OrderStatus model)
        {
            return PostAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "OrderStatus",
                Data = model,
                Token = token
            });
        }

        public async Task<APIResponse> GetListOrderStatusByIdAsync(string token, int id)
        {
            return await GetListByIdOrderStatus(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "OrderStatus/" + id,
                Token = token
            });
        }

        public async Task<APIResponse> UpdateAsync(string token, OrderStatus model)
        {
            return await PutAsync(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "OrderStatus/" + model.OrderStatusId,
                Data = model,
                Token = token
            });
        }

        public async Task<APIResponse> DeleteAsync(string token, int id)
        {
            return await DeleteAsync(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "OrderStatus/" + id,
                Token = token
            });
        }
    }
}
