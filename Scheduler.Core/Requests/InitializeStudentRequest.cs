using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class InitializeStudentRequest
    {
        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 40, MinimumLength = 1)]
        public string SecondName { get; set; }
    }
}
