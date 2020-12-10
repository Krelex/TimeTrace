using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TimeTraceConfiguration;
using TimeTraceInfrastructure.HttpController;
using TimeTraceMVC.Models;
using TimeTraceMVC.Models.Enum;
using TimeTraceService.Application;
using TimeTraceService.Application.Dto;
using TimeTraceService.Application.Enum;
using TimeTraceService.Application.Models;
using X.PagedList;

namespace TimeTraceMVC.Controllers
{
	public class HomeController : MvcControllerBase
	{
		#region Fields

		private readonly ILogger<HomeController> _logger;
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		private readonly KeycloakSettings _keycloakSettings;

		#endregion

		#region Constructor

		public HomeController(ILogger<HomeController> logger, IApplicationService applicationService, IMapper mapper, IOptions<KeycloakSettings> keycloakSettings)
		{
			_logger = logger;
			_applicationService = applicationService;
			_mapper = mapper;
			_keycloakSettings = keycloakSettings.Value;
		}

		#endregion

		#region Actions

		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			GetResultsRequest request = CreateServiceRequest<GetResultsRequest>();
			GetResultsResponse response = _applicationService.GetResults(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			ResultViewModel.ResetRowCounter();
			List<ResultViewModel> viewResults = _mapper.Map<List<ResultViewModel>>(response.Results);

			IPagedList<ResultViewModel> pagedResults = viewResults.ToPagedList(page, pageSize);
			ViewBag.OnePageOfProducts = pagedResults;
			ViewBag.ResourceControler = ResourceEnum.Index;

			return View();
		}

		[Authorize("TimeTraceAdmin")]
		public IActionResult Privacy(int page = 1, int pageSize = 5)
		{
			GetPendingResultsRequest request = CreateServiceRequest<GetPendingResultsRequest>();
			GetPendingResultsResponse response = _applicationService.GetPendingResults(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			ResultViewModel.ResetRowCounter();
			List<ResultViewModel> viewResults = _mapper.Map<List<ResultViewModel>>(response.Results);

			IPagedList<ResultViewModel> pagedResults = viewResults.ToPagedList(page, pageSize);
			ViewBag.OnePageOfProducts = pagedResults;
			ViewBag.ResourceControler = ResourceEnum.Admin;

			return View();
		}

		[Authorize("TimeTraceAdmin")]
		public IActionResult Deactivate(int resultId)
		{
			DeactivateResultRequest request = CreateServiceRequest<DeactivateResultRequest>();
			request.ResultId = resultId;

			DeactivateResultResponse response = _applicationService.DeactivateResult(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			return RedirectToAction("Index");
		}

		[Authorize("TimeTraceAdmin")]
		public IActionResult Approve(int resultId, StatusEnum statusId)
		{
			ApproveResultRequest request = CreateServiceRequest<ApproveResultRequest>();
			request.ResultId = resultId;
			request.Status = statusId;

			ApproveResultResponse response = _applicationService.ApproveResult(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			return RedirectToAction("Privacy");
		}

		[HttpPost]
		public IActionResult Create(ResultViewModel result)
		{
			CreateResultRequest request = CreateServiceRequest<CreateResultRequest>();
			request.ResultDto = _mapper.Map<ResultDto>(result);
			CreateResultResponse response = _applicationService.CreateResult(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			return RedirectToAction("Index");
		}

		public IActionResult Logout()
		{
			string accessToken = HttpContext.GetTokenAsync("access_token").Result;
			string refreshToken = HttpContext.GetTokenAsync("refresh_token").Result;
			LogoutRequest request = new LogoutRequest(_keycloakSettings.Endpoint, _keycloakSettings.Realm, _keycloakSettings.ClientProtocol,
										accessToken, refreshToken, _keycloakSettings.ClientId, _keycloakSettings.ClientSecret);

			LogoutResponse response = _applicationService.Logout(request).Result;

			if (!response.Success)
				return RedirectToAction("Error");

			Request.Cookies.Keys.ToList().ForEach(x => Response.Cookies.Delete(x));
			AuthenticationHttpContextExtensions.SignOutAsync(HttpContext).Wait();
			//HttpContext.SignOutAsync("OpenIdConnect").Wait();

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		#endregion

	}
}
