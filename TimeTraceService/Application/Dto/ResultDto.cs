using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraceService.Application.Dto
{
    public class ResultDto
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan RaceTime { get; set; }
        public bool IsApproved { get; set; }
    }
}
