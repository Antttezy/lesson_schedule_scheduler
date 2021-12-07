using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class SubjectsListResponse : ResponseBase
    {
        public SubjectsListResponse()
        {
        }

        public SubjectsListResponse(params string[] errors) : base(errors)
        {
        }

        public int Page { get; set; }

        public int MaxPage { get; set; }

        public IEnumerable<SubjectObject> Subjects { get; set; }
    }
}
