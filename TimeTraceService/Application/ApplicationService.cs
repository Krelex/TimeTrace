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
using TimeTraceService.Application.Enum;
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

                Result user = _mapper.Map<Result>(request.ResultDto);
                await _applicationContext.Result.AddAsync(user);
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
                List<Result> results = await _applicationContext.Result
                                                                .AsNoTracking()
                                                                .Where(x => x.Active == true && x.StatusId == (int)StatusEnum.Approved)
                                                                .ToListAsync();

                response.Results = _mapper.Map<List<ResultDto>>(results);
                response.Success = true;
            }
            catch (Exception ex)
            {
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
                                                                .ToListAsync();

                response.Results = _mapper.Map<List<ResultDto>>(results);
                response.Success = true;
            }
            catch (Exception ex)
            {
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
                Result result = await _applicationContext.Result.Where(x => x.Id == request.ResultId).SingleAsync();
                result.StatusId = (int)request.Status;

                await _applicationContext.SaveChangesAsync();

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
