using System.Collections.Generic;
using TimeTraceInfrastructure.Messaging;
using TimeTraceService.Application.Dto;

namespace TimeTraceService.Application.Models
{
    public class GetResultsRequest : RequestBase
    {
    }

    public class GetResultsResponse : ResponseBase<GetResultsRequest>
    {
        public List<ResultDto> Results { get; set; }
    }
}
