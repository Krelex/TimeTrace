using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using TimeTraceConfiguration;
using TimeTraceDataAccess.ApplicationContext;
using TimeTraceMVC.Mapping;
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

			MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new TimeTraceMappingProfile());
			});

			services.AddSingleton(mappingConfig.CreateMapper());
		}

		private static void ConfigureKeycloak(IServiceCollection services, IConfiguration configuration)
		{
			KeycloakSettings settings = configuration.GetSection(nameof(KeycloakSettings)).Get<KeycloakSettings>();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddCookie()
			.AddOpenIdConnect(options =>
			{
				options.Authority = settings.AuthorityUrl;
				options.ClientId = settings.ClientId;
				options.ClientSecret = settings.ClientSecret;
				options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
				options.SaveTokens = true;
				options.GetClaimsFromUserInfoEndpoint = true;
				options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
			});
		}
	}
}
