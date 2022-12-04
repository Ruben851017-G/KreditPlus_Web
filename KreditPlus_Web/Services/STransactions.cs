using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;

namespace KreditPlus_Web.Services
{
    public class STransactions: BaseService, ITransactions
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        public STransactions(IHttpClientFactory clientFactory, IConfiguration config, ILogger<STransactions> logger) : base(clientFactory)
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
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Transactions",
                Token = token
            });
        }
        public Task<T> GetByDateAsync<T>(string token, Transactions model)
        {
            return SendByDateAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = _configuration.GetValue<string>("ServiceAPI:API") + "Transactions",
                Data = model,
                Token = token
            });
        }
    }
}
