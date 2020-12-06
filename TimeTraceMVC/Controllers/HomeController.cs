using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger, IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService;
        }

        #endregion

        #region Actions

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var request = CreateServiceRequest<CreateScoreRequest>();
            _applicationService.CreateScore(request);



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
