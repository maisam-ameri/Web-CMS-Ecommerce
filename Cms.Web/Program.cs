using Cms.DataLayer.Context;
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

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    app.MapRazorPages();
});

app.Run();
