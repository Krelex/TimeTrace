using TimeTraceInfrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TimeTraceInfrastructure.HttpController
{
    public abstract class MvcControllerBase : Controller
    {
        protected MvcControllerBase() : base()
        {
            return;
        }

        protected virtual string UserToken 
        {
            get
            {
                Claim claim = (Claim)null;
                if (((ControllerBase)this).User.Identity is ClaimsIdentity identity)
                    claim = identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                return claim == null ? string.Empty : claim.Value;
            }
        }
        protected virtual Guid RequestToken 
        {
            get
            {
                StringValues stringValues;
                return ((IDictionary<string, StringValues>)((ControllerBase)this).Request.Headers).TryGetValue(nameof(RequestToken), out stringValues) ? new Guid(stringValues[0]) : Guid.NewGuid();
            }
        }

        protected TRequest CreateServiceRequest<TRequest>() where TRequest : RequestBase, new() 
        {
            TRequest request = new TRequest();
            request.RequestToken = this.RequestToken;
            request.UserToken = this.UserToken;
            return request;
        }

    }
}
