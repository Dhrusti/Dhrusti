using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using MVC_Project;

var builder = WebApplication.CreateBuilder(args);

// Add Connection String 
string connString = builder.Configuration.GetConnectionString("EntitiesConnection");
builder.Services.AddDbContext<RevenueCycleDbContext>(options =>
{
	options.UseSqlServer(connString);
});

// To Allow Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.DIScopes();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
