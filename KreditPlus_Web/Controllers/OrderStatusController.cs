using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KreditPlus_Web.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration;
        private readonly IOrderStatus _orderstatusService;

        public OrderStatusController(IConfiguration config, ILogger<OrderStatusController> logger, IOrderStatus orderstatusService)
        {
            _configuration = config;
            _logger = logger;
            _orderstatusService = orderstatusService;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderStatus> list = new();
            list = await GetData();
            return View(list);
        }

        public IActionResult Create()
        {
            OrderStatus model = new OrderStatus();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderStatus model)
        {
            List<OrderStatus> data = await GetData();

            var exist = data.Where(m => m.OrderStatusDesc.ToLower() == model.OrderStatusDesc.ToLower()).FirstOrDefault();

            if (exist != null)
            {
                TempData["Message"] = "Nama status sudah digunakan, silahkan cari yah lain";
            }

            else if (model.OrderStatusDesc == null)
            {
                TempData["Message"] = "Please fill the blank form";
            }
            else
            {
                var response = await _orderstatusService.CreateAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken), model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Message"] = "Success add new menu";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            OrderStatus list = new();
            list = await GetDataById(id);
            return View(list);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderStatus model)
        {
            if (model.OrderStatusDesc == null)
            {
                TempData["Message"] = "Please fill the blank form";
                return View(model);
            }

            var response = await _orderstatusService.UpdateAsync(HttpContext.Session.GetString(SD.SessionToken), model);

            if (response != null && response.IsSuccess)
            {
                TempData["Message"] = "Success update order status";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderstatusService.DeleteAsync(HttpContext.Session.GetString(SD.SessionToken), id);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Message"] = "Data has been deleted";
            }
            else
            {
                TempData["Message"] = "There is some error while processing";
            }
            return RedirectToAction("Index");
        }

        #region GetData
        private async Task<List<OrderStatus>> GetData()
        {
            List<OrderStatus> data = new();
            var response = await _orderstatusService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<OrderStatus>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        private async Task<OrderStatus> GetDataById(int id)
        {
            OrderStatus data = new();
            var response = await _orderstatusService.GetListOrderStatusByIdAsync(HttpContext.Session.GetString(SD.SessionToken), id);
            if (response != null && response.IsSuccess)
            {
                data = (OrderStatus)response.Data;
            }
            return data;
        }
        #endregion
    }
}
