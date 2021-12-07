using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class GroupMembersResponse : ResponseBase
    {
        public GroupMembersResponse()
        {
        }

        public GroupMembersResponse(params string[] errors) : base(errors)
        {
        }

        public IEnumerable<StudentObject> Students { get; set; }
    }
}
