using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Group
    {
        [Required]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; }

        public ICollection<Workload> Workloads { get; }

        public Group()
        {
            Students = new List<Student>();
            Workloads = new List<Workload>();
        }
    }
}
