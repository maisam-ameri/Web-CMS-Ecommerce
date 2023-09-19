using Cms.Core.Convertors;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



#region Database Context
builder.Services.AddDbContext<CmsContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("CmsConnection"));
});
#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});

#endregion


#region Ioc

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllerRoute(
            name: "userPanel",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

    app.MapRazorPages();

});

app.Run();
