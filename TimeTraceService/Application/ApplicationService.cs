using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTraceDataAccess.ApplicationContext;
using TimeTraceDataAccess.ApplicationContext.Models;
using TimeTraceInfrastructure.Services;
using TimeTraceService.Application.Dto;
using TimeTraceService.Application.Models;

namespace TimeTraceService.Application
{
    public class ApplicationService : DomainServiceBase, IApplicationService
    {
        #region Fields

        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ApplicationService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
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

                UserTime user = new UserTime()
                {
                    FirstName = "Fico",
                    LastName = "Fix",
                    RaceTime = new TimeSpan(1,5,3)
                };
                await _applicationContext.UserTime.AddAsync(user);
                await _applicationContext.SaveChangesAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
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
                List<UserTime> results = await _applicationContext.UserTime.ToListAsync();

                response.Results = _mapper.Map<List<ResultDto>>(results);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response = GenericException<GetResultsRequest, GetResultsResponse>(response, ex);
            }

            return response;
        }

        public async Task<GetPendingResultResponse> GetPendingResults(GetPendingResultRequest request)
        {
            GetPendingResultResponse response = new GetPendingResultResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };

            try
            {

                response.Success = true;
            }
            catch (Exception ex)
            {
                response = GenericException<GetPendingResultRequest, GetPendingResultResponse>(response, ex);
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

                response.Success = true;
            }
            catch (Exception ex)
            {
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

                response.Success = true;
            }
            catch (Exception ex)
            {
                response = GenericException<ApproveResultRequest, ApproveResultResponse>(response, ex);
            }

            return response;
        }

        #endregion
    }
}
