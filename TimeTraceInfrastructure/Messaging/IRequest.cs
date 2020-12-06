using System;

namespace TimeTraceInfrastructure.Messaging
{
    public interface IRequest
    {
        Guid RequestToken { get; set; }
    }
}
