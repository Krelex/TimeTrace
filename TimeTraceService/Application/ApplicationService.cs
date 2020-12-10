using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeTraceDataAccess.ApplicationContext;
using TimeTraceDataAccess.ApplicationContext.Models;
using TimeTraceInfrastructure.Services;
using TimeTraceService.Application.Dto;
using TimeTraceService.Application.Enum;
using TimeTraceService.Application.Models;

namespace TimeTraceService.Application
{
	public class ApplicationService : DomainServiceBase, IApplicationService
	{
		#region Fields

		private readonly ApplicationContext _applicationContext;
		private readonly IMapper _mapper;
		private readonly ILogger<ApplicationService> _logger;

		#endregion

		#region Constructor

		public ApplicationService(ApplicationContext applicationContext, IMapper mapper, ILogger<ApplicationService> logger)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
			_logger = logger;
		}

		#endregion

		#region IApplicationService

		public async Task<CreateResultResponse> CreateResult(CreateResultRequest request)
		{
			CreateResultResponse response = new CreateResultResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{

				Result user = _mapper.Map<Result>(request.ResultDto);
				await _applicationContext.Result.AddAsync(user);
				await _applicationContext.SaveChangesAsync();

				_logger.LogInformation($"Result {user.Id} is successfully  created.");

				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<CreateResultRequest, CreateResultResponse>(response, ex);
			}

			return response;
		}

		public async Task<GetResultsResponse> GetResults(GetResultsRequest request)
		{
			GetResultsResponse response = new GetResultsResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{
				List<Result> results = await _applicationContext.Result
																.AsNoTracking()
																.Where(x => x.Active == true && x.StatusId == (int)StatusEnum.Approved)
																.OrderBy(x => x.RaceTime)
																.ToListAsync();

				response.Results = _mapper.Map<List<ResultDto>>(results);
				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<GetResultsRequest, GetResultsResponse>(response, ex);
			}

			return response;
		}

		public async Task<GetPendingResultsResponse> GetPendingResults(GetPendingResultsRequest request)
		{
			GetPendingResultsResponse response = new GetPendingResultsResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{
				List<Result> results = await _applicationContext.Result
																.AsNoTracking()
																.Where(x => x.Active == true && x.StatusId == (int)StatusEnum.Pending)
																.OrderBy(x => x.RaceTime)
																.ToListAsync();

				response.Results = _mapper.Map<List<ResultDto>>(results);
				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<GetPendingResultsRequest, GetPendingResultsResponse>(response, ex);
			}

			return response;
		}

		public async Task<DeactivateResultResponse> DeactivateResult(DeactivateResultRequest request)
		{
			DeactivateResultResponse response = new DeactivateResultResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{
				Result result = await _applicationContext.Result.Where(x => x.Id == request.ResultId).SingleAsync();
				result.Active = false;

				await _applicationContext.SaveChangesAsync();

				_logger.LogInformation($"Result {result.Id} successfully deactivate");

				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<DeactivateResultRequest, DeactivateResultResponse>(response, ex);
			}

			return response;
		}

		public async Task<ApproveResultResponse> ApproveResult(ApproveResultRequest request)
		{
			ApproveResultResponse response = new ApproveResultResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{
				Result result = await _applicationContext.Result.Where(x => x.Id == request.ResultId).SingleAsync();
				result.StatusId = (int)request.Status;

				if (request.Status == StatusEnum.Declined)
					result.Active = false;

				await _applicationContext.SaveChangesAsync();

				_logger.LogInformation($"Result {result.Id} is successfully {request.Status.ToString()} ");

				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<ApproveResultRequest, ApproveResultResponse>(response, ex);
			}

			return response;
		}

		public async Task<LogoutResponse> Logout(LogoutRequest request)
		{
			LogoutResponse response = new LogoutResponse()
			{
				Request = request,
				ResponseToken = Guid.NewGuid()
			};

			try
			{
				string requestUrl = $"{request.EndpointAddress}/auth/realms/{request.RealmName}/protocol/{request.Protocol}/logout";

				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("refresh_token", request.RefreshToken),
					new KeyValuePair<string, string>("client_id", request.ClientId),
					new KeyValuePair<string, string>("client_secret", request.ClientSecret)
				};

				FormUrlEncodedContent content = new FormUrlEncodedContent(list);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
				{
					CharSet = "UTF-8"
				};

				HttpRequestMessage httpRequest = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(requestUrl),
					Content = content
				};

				httpRequest.Headers.Add("Authorization", $"Bearer {request.AccessToken}");

				HttpClient httpClient = new HttpClient();
				HttpResponseMessage responseMessage = await httpClient.SendAsync(httpRequest);

				_logger.LogInformation($"Access {request.AccessToken} is successfully logout from Keycloak");

				response.Success = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				response = GenericException<LogoutRequest, LogoutResponse>(response, ex);
			}

			return response;
		}

		#endregion
	}
}
