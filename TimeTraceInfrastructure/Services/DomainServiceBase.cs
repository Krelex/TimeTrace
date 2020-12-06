using System;
using TimeTraceInfrastructure.Messaging;

namespace TimeTraceInfrastructure.Services
{
    public abstract class DomainServiceBase
    {
        protected TResponse GenericException<TRequest, TResponse>(TResponse response, Exception exception)
            where TRequest : RequestBase
            where TResponse : ResponseBase<TRequest>
        {
            response.Success = false;
            response.Message = exception.Message;
            return response;
        }
    }
}
