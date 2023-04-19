using CorePush.Apple;
using CorePush.Google;
using DataLayer.Entities;
using DTO.ReqDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WaltCapitalManagementWebAPI;
using WaltCapitalManagementWebAPI.Filters;
using WaltCapitalManagementWebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add Connection String 
string connString = builder.Configuration.GetConnectionString("EntitiesConnection");
builder.Services.AddDbContext<WaltCapitalDBContext>(options =>
{
    options.UseSqlServer(connString);
});

// To Allow Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Add Authorization Option In swagger
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

builder.Services.AddSignalR();
builder.Services.DIScopes();

builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();

// Configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<NotificationSettingsReqDTO>(appSettingsSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value)),
            ValidIssuer = builder.Configuration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
            ValidAudience = builder.Configuration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
            ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration.GetSection("JsonWebTokenKeys:TokenExpiryMin").Value)),
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ExceptionFilter>();
    config.Filters.Add<AuthorizationFilter>();
    config.Filters.Add<ActionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
if (Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:GlobalAuthantication").Value))
    app.MapControllers().RequireAuthorization();
else
    app.MapControllers();

app.MapHub<ChatHub>("/hubs/chat");
app.Run();