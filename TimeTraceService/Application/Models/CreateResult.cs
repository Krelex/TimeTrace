using TimeTraceInfrastructure.Messaging;
using TimeTraceService.Application.Dto;

namespace TimeTraceService.Application.Models
{
    public class CreateResultRequest : RequestBase
    {
        public ResultDto ResultDto { get; set; }
    }

    public class CreateResultResponse : ResponseBase<CreateResultRequest>
    {
    }
}
