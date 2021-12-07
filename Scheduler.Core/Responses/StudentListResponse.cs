using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class StudentListResponse : ResponseBase
    {
        public StudentListResponse()
        {
        }

        public StudentListResponse(params string[] errors) : base(errors)
        {
        }

        public int Page { get; set; }

        public int MaxPage { get; set; }

        public IEnumerable<StudentObject> Students { get; set; }
    }
}
