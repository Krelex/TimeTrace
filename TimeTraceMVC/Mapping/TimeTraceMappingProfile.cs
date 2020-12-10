using AutoMapper;
using TimeTraceDataAccess.ApplicationContext.Models;
using TimeTraceMVC.Models;
using TimeTraceService.Application.Dto;

namespace TimeTraceMVC.Mapping
{
    public class TimeTraceMappingProfile : Profile
    {
        public TimeTraceMappingProfile()
        {
            CreateMap<Result, ResultDto>();
            CreateMap<ResultDto, Result>();

            CreateMap<ResultDto, ResultViewModel>();
            CreateMap<ResultViewModel, ResultDto>();
        }
    }
}
