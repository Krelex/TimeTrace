using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTraceService.Application.Models;

namespace TimeTraceService.Application
{
    public interface IApplicationService
    {
        Task<CreateScoreResponse> CreateScore(CreateScoreRequest request);
    }
}
