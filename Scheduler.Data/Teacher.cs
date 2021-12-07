using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string SecondName { get; set; }

        [Required]
        public Degree Degree { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime CareerStarted { get; set; }
    }
}
