using System.Collections.Generic;
using TimeTraceInfrastructure.Messaging;
using TimeTraceService.Application.Dto;

namespace TimeTraceService.Application.Models
{
    public class GetPendingResultsRequest : RequestBase
    {
    }

    public class GetPendingResultsResponse : ResponseBase<GetPendingResultsRequest>
    {
        public List<ResultDto> Results { get; set; }
    }
}
