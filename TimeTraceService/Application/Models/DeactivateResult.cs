using TimeTraceInfrastructure.Messaging;


namespace TimeTraceService.Application.Models
{
    public class DeactivateResultRequest : RequestBase
    {
        public int ResultId { get; set; }
    }

    public class DeactivateResultResponse : ResponseBase<DeactivateResultRequest>
    {
    }
}
