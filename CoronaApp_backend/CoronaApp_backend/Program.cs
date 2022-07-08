using Microsoft.EntityFrameworkCore;
using CoronaApp_DbInfo;
using Serilog;
using CoronaApp_backend.Middleware;

var builder = WebApplication.CreateBuilder(args);

AddCors();
ConfigureLogs();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CoronavirusCertificatesDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAdditionalLogMidlleware();
app.MapControllers();
app.Run();

#region helper
void AddCors()
{
	builder.Services.AddCors(options => {
		options.AddDefaultPolicy(policy => {
				policy
					.WithOrigins(builder.Configuration.GetSection("CORS:AllowedOriginsList").Get<string[]>())
					.WithMethods(builder.Configuration.GetSection("CORS:AllowedMethodsList").Get<string[]>())
					.WithHeaders(builder.Configuration.GetSection("CORS:AllowedHeadersList").Get<string[]>());
		});
	});
}

void ConfigureLogs()
{
	IConfigurationRoot ñonfig = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

	Log.Logger = new LoggerConfiguration()
		.ReadFrom.Configuration(ñonfig)
		.CreateLogger();
}
#endregion