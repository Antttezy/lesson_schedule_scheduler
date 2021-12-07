namespace Scheduler.Core.Responses
{
    public class SetStudentGroupResponse : ResponseBase
    {
        public SetStudentGroupResponse()
        {
        }

        public SetStudentGroupResponse(params string[] errors) : base(errors)
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
