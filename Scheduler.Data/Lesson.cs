using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Lesson
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int WorkloadId { get; set; }

        [Required]
        public Workload Workload { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        public DateTime LessonTime { get; set; }
    }
}
