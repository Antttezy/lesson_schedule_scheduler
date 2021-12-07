using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Workload
    {
        [Required]
        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<Group> Groups { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Hours { get; set; }

        public Workload()
        {
            Groups = new List<Group>();
        }
    }
}
