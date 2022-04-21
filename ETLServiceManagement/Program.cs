using ETLServiceManagement.Models;
using ETLServiceManagement.Models.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//db connection string

builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ETL_DBConnection"), sqlServerOptions => sqlServerOptions.CommandTimeout(60)));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddScoped<IServiceRepository, SQLServiceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}                           

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//shehzad
app.UseMvcWithDefaultRoute();
//comment by shahzad
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
