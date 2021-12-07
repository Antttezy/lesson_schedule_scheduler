namespace Scheduler.Core.Responses
{
    public class StudentResponse : ResponseBase
    {
        public StudentResponse()
        {
        }

        public StudentResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string GroupName { get; set; }
    }
}
