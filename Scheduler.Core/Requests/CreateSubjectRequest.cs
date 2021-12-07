using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class CreateSubjectRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
