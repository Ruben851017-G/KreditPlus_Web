using KreditPlus_Web.Interface;
using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace KreditPlus_Web.Controllers
{
    public class UsersController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IUserType _iuserType;
        private readonly IUsers _iusers;
        public UsersController(IConfiguration config, ILogger<UsersController> logger, IUserType iuserType, IUsers iusers)
        {
            _configuration = config;
            _iuserType = iuserType;
            _logger = logger;
            _iusers = iusers;
        }
        public async Task<IActionResult> Index()
        {
            List<Users> list = new();
            list = await GetDataUser();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Users model = new Users();
            ViewBag.ListUserType = await GetDataUserType();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Users model)
        {
            if (model.UserType == null)
            {
                TempData["Message"] = "Please fill the blank form"; 
            }
            List<Users> data = await GetDataUser();

            var exist = data.Where(m => m.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault();

            if (exist != null)
            {
                TempData["Message"] = "user name sudah digunakan, silahkan cari yah lain";
            }

            else
            {
                var response = await _iusers.CreateAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken), model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Message"] = "success add new user";
                    _logger.LogInformation("Register User success :" + model.UserName);
                }
            }

            ViewBag.ListUserType = await GetDataUserType();
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            Users model = new Users();
            ViewBag.ListUserType = await GetDataUserType();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Users model)
        {
            List<Users> data = await GetDataUser();

            var exist = data.Where(m => m.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault();

            if(exist != null)
            {
                TempData["Message"] = "user name sudah digunakan, silahkan cari yah lain";
            }

            else
            {
                var response = await _iusers.CreateAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken), model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Message"] = "success add new user";
                    _logger.LogInformation("Register User success :" + model.UserName);                 
                }
            }
            ViewBag.ListUserType = await GetDataUserType();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Users list = new();
            //ViewBag.ClaimUserId = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "user_id").Value;
            ViewBag.SelectedUserId = id.ToString();
            list = await GetListUserByIdAsync(id);
            ViewBag.ListUserType = await GetDataUserType(); 
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Users model)
        {
            if (model.UserType == null)
            {
                TempData["Message"] = "Please fill the blank form";
                ViewBag.ListUserType = await GetDataUserType();
                return View(model);
            }

            var response = await _iusers.UpdateAsync(HttpContext.Session.GetString(SD.SessionToken), model);

            if (response != null && response.IsSuccess)
            {
                TempData["Message"] = "Success update user";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _iusers.DeleteAsync(HttpContext.Session.GetString(SD.SessionToken), id);
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
        private async Task<List<UserType>> GetDataUserType()
        {
            List<UserType> data = new();
            var response = await _iuserType.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<UserType>>(Convert.ToString(response.Data));
            }
            return data.ToList();

        }
        private async Task<Users> GetListUserByIdAsync(int id)
        {
            Users data = new();
            var response = await _iusers.GetListUserByIdAsync(HttpContext.Session.GetString(SD.SessionToken), id);
            if (response != null && response.IsSuccess)
            {
                data = (Users)response.Data;
            }
            return data;
        }
        private async Task<List<Users>> GetDataUser()
        {
            List<Users> data = new();
            var response = await _iusers.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<List<Users>>(Convert.ToString(response.Data));
            }
            return data.ToList();

        }
        #endregion
    }
}
