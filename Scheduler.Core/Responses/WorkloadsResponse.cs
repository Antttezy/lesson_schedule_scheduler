using Scheduler.Core.Objects;
using System.Collections.Generic;

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
