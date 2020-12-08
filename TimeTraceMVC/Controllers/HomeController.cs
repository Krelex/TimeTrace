using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using TimeTraceInfrastructure.HttpController;
using TimeTraceMVC.Models;
using TimeTraceService.Application;
using TimeTraceService.Application.Dto;
using TimeTraceService.Application.Enum;
using TimeTraceService.Application.Models;

namespace TimeTraceMVC.Controllers
{
    public class HomeController : MvcControllerBase
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger, IApplicationService applicationService, IMapper mapper)
        {
            _logger = logger;
            _applicationService = applicationService;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            GetResultsRequest request = CreateServiceRequest<GetResultsRequest>();
            GetResultsResponse response = _applicationService.GetResults(request).Result;

            if (!response.Success)
                return View("Error");

            List<ResultViewModel> viewResults = _mapper.Map<List<ResultViewModel>>(response.Results) ;

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            GetPendingResultsRequest request = CreateServiceRequest<GetPendingResultsRequest>();
            GetPendingResultsResponse response = _applicationService.GetPendingResults(request).Result;

            if (!response.Success)
                return View("Error");

            List<ResultViewModel> viewResults = _mapper.Map<List<ResultViewModel>>(response.Results);

            return View();
        }

        public IActionResult Deactivate(int resultId)
        {
            DeactivateResultRequest request = CreateServiceRequest<DeactivateResultRequest>();
            request.ResultId = resultId;

            DeactivateResultResponse response = _applicationService.DeactivateResult(request).Result;

            if (!response.Success)
                return View("Error");

            return View("Privacy");
        }

        public IActionResult Approve(int resultId, int statusId)
        {
            ApproveResultRequest request = CreateServiceRequest<ApproveResultRequest>();
            request.ResultId = resultId;
            request.Status = (StatusEnum)statusId;

            ApproveResultResponse response = _applicationService.ApproveResult(request).Result;

            if (!response.Success)
                return View("Error");

            return View("Privacy");
        }

        [HttpPost]
        public IActionResult Create(ResultViewModel result)
        {
            CreateResultRequest request = CreateServiceRequest<CreateResultRequest>();
            request.ResultDto = _mapper.Map<ResultDto>(result);
            CreateResultResponse response = _applicationService.CreateResult(request).Result;

            if (!response.Success)
                return View("Error");

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}
