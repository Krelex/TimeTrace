namespace TimeTraceConfiguration
{
	public class KeycloakSettings
	{
		public string Realm { get; set; }
		public string ClientId { get; set; }
		public string ClientProtocol { get; set; }
		public string ClientSecret { get; set; }
		public string Endpoint { get; set; }
		public string AuthorityUrl { get; set; }
		public string EditAccountUrl { get; set; }
		public bool RequireHttpsMetadata { get; set; }
		public bool IncludeErrorDetails { get; set; }
		public bool ValidateAudience { get; set; }
		public bool ValidateIssuerSigningKey { get; set; }
		public bool ValidateIssuer { get; set; }
		public bool ValidateLifetime { get; set; }
	}
}
