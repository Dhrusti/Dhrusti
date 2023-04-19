using DataLayer.Entities;
using Helper.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI;
using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add Connection String 
string connString = builder.Configuration.GetConnectionString("EntitiesConnection");
builder.Services.AddDbContext<RevenueCycleDbContext>(options =>
{
    options.UseSqlServer(connString);
});

// To Allow Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey =Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:ValidateIssuerSigningKey").Value),
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value)),

//            ValidateIssuer = Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:ValidateIssuer").Value),
//            ValidIssuer = builder.Configuration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,

//            ValidateAudience = Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:ValidateAudience").Value),
//            ValidAudience = builder.Configuration.GetSection("JsonWebTokenKeys:ValidAudience").Value,

//            ValidateLifetime = Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:ValidateLifetime").Value),
//            ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration.GetSection("JsonWebTokenKeys:TokenExpiryMin").Value)),
//        };
//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
//                {
//                    context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
//                }
//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = Convert.ToBoolean(builder.Configuration["JWTTokenSettings:ValidateIssuerSigningKey"]),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTTokenSettings:IssuerSigningKey"])),

        ValidateIssuer = Convert.ToBoolean(builder.Configuration["JWTTokenSettings:ValidateIssuer"]),
        ValidIssuer = builder.Configuration["JWTTokenSettings:ValidIssuer"],

        ValidateAudience = Convert.ToBoolean(builder.Configuration["JWTTokenSettings:ValidateAudience"]),
        ValidAudience = builder.Configuration["JWTTokenSettings:ValidAudience"],

        ValidateLifetime = Convert.ToBoolean(builder.Configuration["JWTTokenSettings:ValidateLifetime"]),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JWTTokenSettings:TokenExpiryMin"])),
    };
});

builder.Services.DIScopes();

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ExceptionFilter>();
    config.Filters.Add<AuthorizationFilter>();
    config.Filters.Add<ActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDirectoryBrowser();

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
//builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();
app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers().RequireAuthorization();

app.MapControllers();

app.MapHub<SignalrHub>("/api/SignalrConnection");
app.Run();
