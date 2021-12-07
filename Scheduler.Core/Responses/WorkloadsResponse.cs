using Scheduler.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Responses
{
    public class WorkloadsResponse : ResponseBase
    {
        public WorkloadsResponse()
        {
        }

        public WorkloadsResponse(params string[] errors) : base(errors)
        {
        }

        public IEnumerable<WorkloadObject> Workloads { get; set; }
    }
}
