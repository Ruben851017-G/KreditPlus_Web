using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;

namespace KreditPlus_Web.Services
{
    public class SOrder: BaseService, IOrder
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;
        public SOrder(IHttpClientFactory clientFactory, IConfiguration config, ILogger<SOrder> logger) : base(clientFactory)
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
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Orders",
                Token = token
            });
        }

        public async Task<APIResponse> UpdateAsync(string token, Order model)
        {
            return await PutAsync(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Orders/" + model.OrderId,
                Data = model,
                Token = token
            });
        }
    }
}
