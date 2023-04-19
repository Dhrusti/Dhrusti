using System.Text;
using DataLayer.Entities;
using ERP_CRM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// To Allow Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.DIScopes();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "ERP-CRM",
		Version = "v1"
	});

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Scheme = "bearer",
		Description = "Please insert JWT token into field"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
	});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8

				.GetBytes(builder.Configuration.GetSection("JwtSettings:JWTKey").Value)),
			ValidateIssuer = false,
			ValidateAudience = false,
			//ValidateLifetime = true
			//ValidateIssuer = true,
			//ValidateAudience = true,
			ValidateLifetime = true,
			// ClockSkew = TimeSpan.FromSeconds(10),
			//ValidateIssuerSigningKey = true,
			//ValidIssuer = Configuration["JwtToken:Issuer"],
			//ValidAudience = Configuration["JwtToken:Issuer"],
			//IssuerSigningKey = new SymmetricSecurityKey(
			// Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"]))
		};
	});

// Add Connection String 
//string connString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ErpDbContext>(options =>
//{
//	options.UseSqlServer(connString);
//});

//var app = builder.Build();


builder.Services.AddDbContext<ErpDbContext>(
  options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseCors();
app.UseRouting();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseSwagger();

app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
	.RequireAuthorization();
//app.MapControllers();

app.Run();
