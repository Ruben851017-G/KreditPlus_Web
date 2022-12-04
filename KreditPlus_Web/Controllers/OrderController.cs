using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KreditPlus_Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration;
        private readonly IOrder _orderService;
        private readonly IUserType _usertypeService;
        private readonly IMenu _menuService;

        public OrderController(IConfiguration config, ILogger<OrderStatusController> logger, IOrder orderService, IUserType usertypeService, IMenu menuService)
        {
            _configuration = config;
            _logger = logger;
            _orderService = orderService;
            _usertypeService = usertypeService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            string _usertype = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "user_type").Value;
            var data = await GetDataUsertype();
            ViewBag.UserType = data.Where(m => m.UserTypeId == Convert.ToInt32(_usertype)).Select(x => x.UserTypeName).SingleOrDefault();
            List<Order> list = new();
            list = await GetData();
            return View(list);
        }
        public async Task<IActionResult> _PartialOrder()
        {
            OrderDetail model = new OrderDetail();
            ViewBag.ListMenu = await GetListMenu();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _PartialOrder(Order model)
        {
            
            return View(model);
        }
        public async Task<IActionResult> ClosedBill(int id)
        {
            var list = await GetData();

            string _closedby = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "user_id").Value;

            var model = list.Where(m => m.OrderId == id).FirstOrDefault();

            model.OrderStatus = Convert.ToInt32(_configuration["ClosedBillParam:Param"]);

            model.ClosedBy = Convert.ToInt32(_closedby);

            var response = await _orderService.UpdateAsync(HttpContext.Session.GetString(SD.SessionToken), model);

            if (response != null && response.IsSuccess)
            {
                TempData["Message"] = "Data has been closed";
            }
            else
            {
                TempData["Message"] = "There is some error while processing";
            }
            return RedirectToAction("Index");
        }

        #region GetData
        private async Task<List<Order>> GetData()
        {
            List<Order> data = new();
            var response = await _orderService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<Order>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        private async Task<List<UserType>> GetDataUsertype()
        {
            List<UserType> data = new();
            var response = await _usertypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<UserType>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        private async Task<List<Menu>> GetListMenu()
        {
            List<Menu> data = new();
            var response = await _menuService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<Menu>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        #endregion
    }
}
