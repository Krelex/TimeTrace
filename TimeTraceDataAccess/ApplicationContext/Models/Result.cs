using System;
using System.Collections.Generic;

#nullable disable

namespace TimeTraceDataAccess.ApplicationContext.Models
{
    public partial class Result
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan RaceTime { get; set; }
        public int StatusId { get; set; }

        public virtual ResultDcstatus Status { get; set; }
    }
}
