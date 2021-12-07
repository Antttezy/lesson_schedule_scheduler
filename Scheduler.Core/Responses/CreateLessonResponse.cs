using Scheduler.Core.Objects;
using System;

namespace Scheduler.Core.Responses
{
    public class CreateLessonResponse : ResponseBase
    {
        public CreateLessonResponse()
        {
        }

        public CreateLessonResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public WorkloadObject Workload { get; set; }
        public TeacherObject Teacher { get; set; }
        public string GroupName { get; set; }
        public DateTime LessonTime { get; set; }
    }
}
