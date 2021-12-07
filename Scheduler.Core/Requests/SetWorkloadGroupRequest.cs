using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class SetWorkloadGroupRequest
    {
        [Required]
        public int WorkloadId { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}
