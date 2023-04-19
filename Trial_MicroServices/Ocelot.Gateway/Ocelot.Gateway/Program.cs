using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile($"Ocelot.json", optional: false, reloadOnChange: true)
	.AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot();
app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
