using System;
using System.Collections.Generic;

#nullable disable

namespace TimeTraceDataAccess.ApplicationContext.Models
{
    public partial class ResultDcstatus
    {
        public ResultDcstatus()
        {
            Result = new HashSet<Result>();
        }

        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Result> Result { get; set; }
    }
}
