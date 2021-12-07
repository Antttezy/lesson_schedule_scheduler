using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class WorkloadListResponse : ResponseBase
    {
        public WorkloadListResponse()
        {
        }

        public WorkloadListResponse(params string[] errors) : base(errors)
        {
        }

        public int Page { get; set; }

        public int MaxPage { get; set; }

        public IEnumerable<WorkloadObject> Workloads { get; set; }
    }
}
