using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class SetStudentGroupRequest
    {
        [Required]
        public int StudentId { get; set; }

        public int? GroupId { get; set; }
    }
}
