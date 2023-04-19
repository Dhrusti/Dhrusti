using BookManagement;
using BusinessLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using ServiceLayer.IService;
using ServiceLayer.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.RegisterDIService();
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookMgtDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IAuthentication, AuthenticationImpl>();
//builder.Services.AddScoped<AuthenticationBLL>();

//builder.Services.AddScoped<ICategory, CategoryImpl>();
//builder.Services.AddScoped<CategoryBLL>();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)app.Environment);
app.Run();
