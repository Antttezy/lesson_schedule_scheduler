using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class GroupInfoResponse : ResponseBase
    {
        public GroupInfoResponse()
        {
        }

        public GroupInfoResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<StudentObject> Students { get; set; }

        public IEnumerable<WorkloadObject> Workloads { get; set; }
    }
}
