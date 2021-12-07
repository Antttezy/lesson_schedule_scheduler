using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public class Student
    {
        [Required]
        public int Id { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string SecondName { get; set; }

        public int? GroupId { get; set; }

        public Group Group { get; set; }
    }
}
