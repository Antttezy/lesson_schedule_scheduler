using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class CreateWorkloadRequest
    {
        public string Description { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int Hours { get; set; }
    }
}
