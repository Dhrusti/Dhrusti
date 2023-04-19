using System.Text;
using Authentication_MicroService;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.DIScopes();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
					Reference = new Microsoft.OpenApi.Models.OpenApiReference {
						Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
							Id = "Bearer"
					}
				},
				new string[] {}
		}
	});
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(option =>
	{
		option.SaveToken = true;
		option.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ClockSkew = TimeSpan.Zero, // it expires token on exact time else token will valid up to 5 minutes.
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Aud"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
		option.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = x =>
			{
				if (x.Exception.GetType() == typeof(SecurityTokenExpiredException))
				{
					x.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
				}
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddDbContext<AuthenticationMicroServiceDbContext>(
  options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
