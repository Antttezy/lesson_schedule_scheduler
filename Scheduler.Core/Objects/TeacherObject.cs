using System;

namespace Scheduler.Core.Objects
{
    public class TeacherObject
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Degree { get; set; }

        public string Position { get; set; }

        public DateTime CareerStarted { get; set; }
    }
}
