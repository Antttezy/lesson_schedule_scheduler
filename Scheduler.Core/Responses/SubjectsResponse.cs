using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class SubjectsResponse : ResponseBase
    {
        public SubjectsResponse()
        {
        }

        public SubjectsResponse(params string[] errors) : base(errors)
        {
        }

        public IEnumerable<SubjectObject> Subjects { get; set; }
    }
}
