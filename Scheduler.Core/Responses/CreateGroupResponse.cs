namespace Scheduler.Core.Responses
{
    public class CreateGroupResponse : ResponseBase
    {
        public CreateGroupResponse()
        {
        }

        public CreateGroupResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
