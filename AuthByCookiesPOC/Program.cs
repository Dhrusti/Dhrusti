using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

if (Convert.ToBoolean(builder.Configuration.GetSection("AuthenticationEnable").Value))
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        x =>
        {
            x.SlidingExpiration = false; // if (true) will work as idea time logout. else, will expire as per expiry time.
            x.ExpireTimeSpan = new TimeSpan(0, 0, 30);
            x.LoginPath = "/api/Auth/Login"; // not necessary
            x.LogoutPath = "/api/Auth/Logout"; // not necessary
            x.Events = new CookieAuthenticationEvents // not necessary (Return 404 Code instead of 401 if not used)
            {
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                }
            };
        });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

if (Convert.ToBoolean(builder.Configuration.GetSection("AuthenticationEnable").Value))
    app.MapControllers().RequireAuthorization();
else
    app.MapControllers();


app.Run();