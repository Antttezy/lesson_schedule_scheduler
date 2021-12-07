using Scheduler.Core.Objects;

namespace Scheduler.Core.Responses
{
    public class SetWorkloadGroupResponse : ResponseBase
    {
        public SetWorkloadGroupResponse()
        {
        }

        public SetWorkloadGroupResponse(params string[] errors) : base(errors)
        {
        }

        public int WorkloadId { get; set; }
        public string WorkloadDescription { get; set; }
        public SubjectObject Subject { get; set; }
        public int Hours { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
