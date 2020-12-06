using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraceInfrastructure.Messaging
{
    public abstract class ResponseBase<T> : IResponse where T : IRequest
    {
        public ResponseBase() { }

        public Guid ResponseToken { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Request { get; set; }
    }
}

