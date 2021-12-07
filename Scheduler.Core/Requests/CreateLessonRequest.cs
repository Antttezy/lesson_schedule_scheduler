using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class CreateLessonRequest
    {
        [Required]
        public int WorkloadId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public long LessonTime { get; set; }
    }
}
