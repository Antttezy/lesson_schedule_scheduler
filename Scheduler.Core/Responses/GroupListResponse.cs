using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class GroupListResponse : ResponseBase
    {
        public GroupListResponse()
        {
        }

        public GroupListResponse(params string[] errors) : base(errors)
        {
        }

        public IEnumerable<GroupObject> Groups { get; set; }
    }
}
