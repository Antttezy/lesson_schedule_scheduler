namespace Scheduler.Core.Responses
{
    public class CreateSubjectResponse : ResponseBase
    {
        public CreateSubjectResponse()
        {
        }

        public CreateSubjectResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
