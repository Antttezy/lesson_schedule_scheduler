using System;

namespace Scheduler.Core.Objects
{
    public class LessonObject
    {
        public int Id { get; set; }
        public WorkloadObject Workload { get; set; }
        public TeacherObject Teacher { get; set; }
        public string GroupName { get; set; }
        public DateTime LessonTime { get; set; }
    }
}
