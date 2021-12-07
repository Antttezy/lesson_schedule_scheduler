using System;

namespace Scheduler.Core.Responses
{
    public class TeacherResponse : ResponseBase
    {
        public TeacherResponse()
        {
        }

        public TeacherResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Degree { get; set; }

        public string Position { get; set; }

        public DateTime CareerStarted { get; set; }
    }
}
