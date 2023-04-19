using Microsoft.EntityFrameworkCore;
using ProactAccouting.CommonHelper;
using ProactAccouting.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProactAccountDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ProactConnectionString_Dev")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<Common>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
