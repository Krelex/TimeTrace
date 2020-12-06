using System;

namespace TimeTraceInfrastructure.Messaging
{
    public abstract class RequestBase : IRequest
    {
        protected RequestBase() { }

        public Guid RequestToken { get; set; }
        public string UserToken { get; set; }
    }
}
