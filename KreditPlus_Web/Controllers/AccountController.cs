using KreditPlus_Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace KreditPlus_Web.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger _logger;
        public IHttpClientFactory httpClient;

        public AccountController(IConfiguration config, ILogger<UsersController> logger, IHttpClientFactory httpClient)
        {
            _configuration = config;
            _logger = logger;
            this.httpClient = httpClient;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginModel model, string returnUrl = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var clients = httpClient.CreateClient("Client");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                string endpoint = _configuration.GetValue<string>("ServiceAPI:API") + "Login";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var result = (JObject)JsonConvert.DeserializeObject(await Response.Content.ReadAsStringAsync());

                    if (result["code"].Value<string>() == "0")
                    {
                        TempData["Message"] = "Invalid Authentication";
                        return View(model);
                    }

                    if (!Response.IsSuccessStatusCode)
                    {
                        ErrorBody error = new ErrorBody();

                        if (((int)Response.StatusCode) == 401)
                        {
                            error.errors = result["code"].Value<string>();

                            TempData["Message"] = error.errors;

                            _logger.LogInformation("login User failed :" + error.errors + model.UserName);

                            return View(model);
                        }

                        error.status_code = result["code"].Value<string>();

                        switch (error.status_code)
                        {
                            case "422":
                                TempData["Message"] = error.message;
                                _logger.LogInformation("login user failed :" + model.UserName);
                                break;
                            default:
                                TempData["Message"] = error.errors;
                                _logger.LogInformation("login User failed :" + error.errors + model.UserName);
                                break;
                        }
                        return View(model);
                    }
                    else
                    {
                        var token = result["data"]["token"].Value<string>();

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        var claims = new List<Claim>
                        {
                            new Claim("user_id", result["data"]["userId"].Value<string>()),
                            new Claim("user_type", result["data"]["userType"].Value<string>()),
                        };

                        var userIdentity = new ClaimsIdentity(claims, "login");

                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(principal);

                        HttpContext.Session.SetString("user_id", Convert.ToString(result["data"]["userId"].Value<string>()));

                        HttpContext.Session.SetString("user_type", Convert.ToString(result["data"]["userType"].Value<string>()));

                        HttpContext.Session.SetString("user_name", Convert.ToString(result["data"]["userName"].Value<string>()));

                        HttpContext.Session.SetString(SD.SessionToken, token);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")

                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            _logger.LogInformation("user has success login :" + model.UserName);

                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
