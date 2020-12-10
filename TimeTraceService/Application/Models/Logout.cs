using TimeTraceInfrastructure.Messaging;

namespace TimeTraceService.Application.Models
{
	public class LogoutRequest : RequestBase
	{
		#region Constructor
		public LogoutRequest(string endpointAddress, string realmName, string protocol, string accessToken, string refreshToken, string clientId, string clientSecret)
		{
			EndpointAddress = endpointAddress;
			RealmName = realmName;
			Protocol = protocol;
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			ClientId = clientId;
			ClientSecret = clientSecret;
		}

		#endregion

		#region Properties

		public string EndpointAddress { get; }
		public string RealmName { get; }
		public string Protocol { get; }
		public string AccessToken { get; }
		public string RefreshToken { get; }
		public string ClientId { get; }
		public string ClientSecret { get; }

		#endregion

	}

	public class LogoutResponse : ResponseBase<LogoutRequest>
	{
	}
}
