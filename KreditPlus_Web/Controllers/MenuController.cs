using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KreditPlus_Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration;
        private readonly IMenu _menuService;
        private readonly IMenuStatus _menustatusService;
        private readonly IStatusMenuType _menustatusTypeService;

        public MenuController(IConfiguration config, ILogger<MenuController> logger, IMenu menuService, IMenuStatus menustatusService, IStatusMenuType menustatusTypeService)
        {
            _configuration = config;
            _logger = logger;
            _menuService = menuService;
            _menustatusService = menustatusService;
            _menustatusTypeService = menustatusTypeService;
        }
        public async Task<IActionResult> Index()
        {
            List<Menu> list = new();
            list = await GetData();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Menu model = new Menu();
            ViewBag.ListStatusMenu = await GetStatusMenu();
            ViewBag.ListMenuType = await GetStatusMenuType();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu model)
        {
            List<Menu> data = await GetData();

            var exist = data.Where(m => m.MenuName.ToLower() == model.MenuName.ToLower()).FirstOrDefault();

            if (exist != null)
            {
                TempData["Message"] = "Nama menu sudah digunakan, silahkan cari yah lain";
            }

            else if (model.StatusMenuId == null || model.MenuTypeId == null)
            {
                TempData["Message"] = "Please fill the blank form";               
            }
            else
            {
                var response = await _menuService.CreateAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken), model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Message"] = "Success add new menu";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ListStatusMenu = await GetStatusMenu();
            ViewBag.ListMenuType = await GetStatusMenuType();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Menu list = new();
            list = await GetDataById(id);
            ViewBag.ListStatusMenu = await GetStatusMenu();
            ViewBag.ListMenuType = await GetStatusMenuType();
            return View(list);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Menu model)
        {
            if (model.StatusMenuId == null || model.MenuTypeId == null)
            {
                TempData["Message"] = "Please fill the blank form";
                ViewBag.ListStatusMenu = await GetStatusMenu();
                ViewBag.ListMenuType = await GetStatusMenuType();
                return View(model);
            }

            var response = await _menuService.UpdateAsync(HttpContext.Session.GetString(SD.SessionToken), model);

            if (response != null && response.IsSuccess)
            {
                TempData["Message"] = "Success update menu";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _menuService.DeleteAsync(HttpContext.Session.GetString(SD.SessionToken), id);
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
        private async Task<List<Menu>> GetData()
        {
            List<Menu> data = new();
            var response = await _menuService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<Menu>>(Convert.ToString(response.Data));
            }
            return data.ToList();
        }
        private async Task<List<StatusMenu>> GetStatusMenu()
        {
            List<StatusMenu> data = new();
            var response = await _menustatusService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<StatusMenu>>(Convert.ToString(response.Data));
            }
            return data.ToList();

        }
        private async Task<Menu> GetDataById(int id)
        {
            Menu data = new();
            var response = await _menuService.GetListByIdAsync(HttpContext.Session.GetString(SD.SessionToken), id);
            if (response != null && response.IsSuccess)
            {
                data = (Menu)response.Data;
            }
            return data;
        }
        private async Task<List<StatusMenuType>> GetStatusMenuType()
        {
            List<StatusMenuType> data = new();
            var response = await _menustatusTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<StatusMenuType>>(Convert.ToString(response.Data));
            }
            return data.ToList();

        }
        #endregion
    }
}
