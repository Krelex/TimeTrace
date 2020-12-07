using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using TimeTraceInfrastructure.HttpController;
using TimeTraceMVC.Models;
using TimeTraceService.Application;
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
            var response = _applicationService.GetResults(request).Result;

            if (!response.Success)
                return View("Error");

            var viewResults = _mapper.Map<List<ResultViewModel>>(response.Results) ;

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            CreateResultRequest request = CreateServiceRequest<CreateResultRequest>();
            CreateResultResponse response = _applicationService.CreateResult(request).Result;

            if (!response.Success)
                return View("Error");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}
