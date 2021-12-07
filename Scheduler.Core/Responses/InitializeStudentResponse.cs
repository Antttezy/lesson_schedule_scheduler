namespace Scheduler.Core.Responses
{
    public class InitializeStudentResponse : ResponseBase
    {
        public InitializeStudentResponse()
        {
        }

        public InitializeStudentResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int? GroupId { get; set; }
    }
}
