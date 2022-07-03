using Microsoft.EntityFrameworkCore;
using CoronaApp_backend.Models;
using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

AddCors();
ConfigureLogs();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection to database
builder.Services.AddDbContext<CoronavirusCertificatesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	//app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CorsAllowSpecific");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

#region helper
void AddCors()
{
	builder.Services.AddCors(options =>
	{
		options.AddPolicy("CorsAllowAll",
			policy =>
			{
				policy
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});

		options.AddPolicy("CorsAllowSpecific",
			policy =>
			{
				policy
					.WithOrigins(builder.Configuration.GetSection("CORS:AllowedOriginsList").Get<string[]>())
					.WithMethods(builder.Configuration.GetSection("CORS:AllowedMethodsList").Get<string[]>())
					.WithHeaders(builder.Configuration.GetSection("CORS:AllowedHeadersList").Get<string[]>());
			});
	});
}

void ConfigureLogs()
{
	string? EnvValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

	IConfigurationRoot Config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

	string dateNow = DateTime.Now.ToString("yyyy_MM_dd");
	//string logDir = AppDomain.CurrentDomain.BaseDirectory + "Logs\\log-" + dateNow + ".log";
	string LogDir = Directory.GetCurrentDirectory() + "\\Logs\\log-" + dateNow + ".log";

	Log.Logger = new LoggerConfiguration()
		.Enrich.FromLogContext()
		.Enrich.WithExceptionDetails()
		.WriteTo.Debug()
		.WriteTo.Console()
		.WriteTo.File(LogDir)
		.WriteTo.Elasticsearch(ConfigureELS(Config, EnvValue))
		.CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot Config, string EnvValue)
{
	return new ElasticsearchSinkOptions(new Uri(Config["ELKConfiguration:Uri"]))
	{
		AutoRegisterTemplate = true,
		IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{EnvValue.ToLower()}-{DateTime.Now:yyyy-MM}"
	};
}
#endregion