namespace Scheduler.Core.Objects
{
    public class WorkloadObject
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public SubjectObject Subject { get; set; }
        public int Hours { get; set; }
    }
}
