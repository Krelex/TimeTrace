using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTraceDataAccess.ApplicationContext;
using TimeTraceDataAccess.ApplicationContext.Models;
using TimeTraceInfrastructure.Services;
using TimeTraceService.Application.Models;

namespace TimeTraceService.Application
{
    public class ApplicationService : DomainServiceBase, IApplicationService
    {
        #region Fields

        private readonly ApplicationContext _applicationContext;

        #endregion

        #region Constructor

        public ApplicationService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }


        #endregion

        #region IApplicationService

        public async Task<CreateScoreResponse> CreateScore(CreateScoreRequest request)
        {
            CreateScoreResponse response = new CreateScoreResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {

                UserTime user = new UserTime()
                {
                    FirstName = "Fico",
                    LastName = "Fix",
                    RaceTime = new TimeSpan(1,5,3)
                };
                await _applicationContext.UserTime.AddAsync(user);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response = GenericException<CreateScoreRequest, CreateScoreResponse>(response, ex);
            }

            return response;
        }

        #endregion
    }
}
