using KreditPlus_Web.Interface;
using KreditPlus_Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("Client", c => c.BaseAddress = new Uri("https://localhost:7289/"));
builder.Services.AddHttpClient<IOrder, SOrder>();
builder.Services.AddScoped<IOrder, SOrder>();
builder.Services.AddHttpClient<IOrderStatus, SOrderStatus>();
builder.Services.AddScoped<IOrderStatus, SOrderStatus>();
builder.Services.AddHttpClient<ITransactions, STransactions>();
builder.Services.AddScoped<ITransactions, STransactions>();
builder.Services.AddHttpClient<IStatusMenuType, SStatusMenuType>();
builder.Services.AddScoped<IStatusMenuType, SStatusMenuType>();
builder.Services.AddHttpClient<IMenu, SMenu>();
builder.Services.AddScoped<IMenu, SMenu>();
builder.Services.AddHttpClient<IMenuStatus, SMenuStatus>();
builder.Services.AddScoped<IMenuStatus, SMenuStatus>();
builder.Services.AddHttpClient<IUserType, SUserType>();
builder.Services.AddScoped<IUserType, SUserType>();
builder.Services.AddHttpClient<IUsers, SUsers>();
builder.Services.AddScoped<IUsers, SUsers>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.Cookie.HttpOnly = true;
    x.LoginPath = "/Account/Login";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    x.AccessDeniedPath = new PathString("/Error");
    x.SlidingExpiration = true;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc(o =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    o.Filters.Add(new AuthorizeFilter(policy));
});

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

builder.Services.AddCors();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
