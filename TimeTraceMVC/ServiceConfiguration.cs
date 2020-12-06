using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTraceConfiguration;
using TimeTraceDataAccess.ApplicationContext;
using TimeTraceService.Application;

namespace TimeTraceMVC
{
	public static class ServiceConfiguration
	{
		public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			ConfigureApplicationServices(services, configuration);
			ConfigureKeycloak(services, configuration);
		}

		private static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IApplicationService, ApplicationService>();
			services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

		}

		private static void ConfigureKeycloak(IServiceCollection services, IConfiguration configuration)
		{
			KeycloakSettings settings = configuration.GetSection(nameof(KeycloakSettings)).Get<KeycloakSettings>();
		}
	}
}
