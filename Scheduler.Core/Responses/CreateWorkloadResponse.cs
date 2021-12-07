using Scheduler.Core.Objects;

namespace Scheduler.Core.Responses
{
    public class CreateWorkloadResponse : ResponseBase
    {
        public CreateWorkloadResponse()
        {
        }

        public CreateWorkloadResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public SubjectObject Subject { get; set; }
        public int Hours { get; set; }
    }
}
