using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;


namespace TimeTraceMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
				ConfigureLogger(config.GetConnectionString("Log"));
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		public static void ConfigureLogger(string? connectionString)
		{
			var colOpts = new ColumnOptions();
			colOpts.DisableTriggers = true;
			colOpts.Store.Remove(StandardColumn.MessageTemplate);
			colOpts.Store.Remove(StandardColumn.Properties);

			var conf = new LoggerConfiguration()
				.MinimumLevel.Information()
				.MinimumLevel.Override(nameof(Microsoft), LogEventLevel.Warning)
				.MinimumLevel.Override(nameof(System), LogEventLevel.Warning);

			conf.WriteTo.MSSqlServer(
				connectionString: connectionString,
				schemaName: "Application",
				tableName: "Log",
				restrictedToMinimumLevel: LogEventLevel.Information,
				period: TimeSpan.FromSeconds(30),
				columnOptions: colOpts);


			Log.Logger = conf.CreateLogger();
		}
	}
}
