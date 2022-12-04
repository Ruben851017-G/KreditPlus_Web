using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KreditPlus_Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration;
        private readonly ITransactions _transactionsService;
        public TransactionsController(IConfiguration config, ILogger<TransactionsController> logger, ITransactions transactionsService)
        {
            _configuration = config;
            _logger = logger;
            _transactionsService = transactionsService;
        }
        public async Task<IActionResult> Index()
        {
            List<Transactions> list = new();
            list = await GetData();
            return View(list);
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        #region GetData
        private async Task<List<Transactions>> GetData()
        {
            List<Transactions> data = new();
            var response = await _transactionsService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<Transactions>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        #endregion
    }
}
