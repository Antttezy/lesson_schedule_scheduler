using Scheduler.Core.Objects;
using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class LessonsResponse : ResponseBase
    {
        public LessonsResponse()
        {
        }

        public LessonsResponse(params string[] errors) : base(errors)
        {
        }

        public IEnumerable<LessonObject> Lessons { get; set; }
    }
}
