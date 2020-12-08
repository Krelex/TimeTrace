using System.Threading.Tasks;
using TimeTraceService.Application.Models;

namespace TimeTraceService.Application
{
    public interface IApplicationService
    {
        Task<CreateResultResponse> CreateResult(CreateResultRequest request);
        Task<GetResultsResponse> GetResults(GetResultsRequest request);
        Task<GetPendingResultsResponse> GetPendingResults(GetPendingResultsRequest request);
        Task<DeactivateResultResponse> DeactivateResult(DeactivateResultRequest request);
        Task<ApproveResultResponse> ApproveResult(ApproveResultRequest request);
    }
}
