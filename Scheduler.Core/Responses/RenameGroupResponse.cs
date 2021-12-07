namespace Scheduler.Core.Responses
{
    public class RenameGroupResponse : ResponseBase
    {
        public RenameGroupResponse()
        {
        }

        public RenameGroupResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
