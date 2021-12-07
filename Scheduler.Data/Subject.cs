using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Subject
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
