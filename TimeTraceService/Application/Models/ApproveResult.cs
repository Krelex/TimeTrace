using TimeTraceInfrastructure.Messaging;
using TimeTraceService.Application.Enum;

namespace TimeTraceService.Application.Models
{
    public class ApproveResultRequest : RequestBase
    {
        public int ResultId { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class ApproveResultResponse : ResponseBase<ApproveResultRequest>
    {
    }
}
