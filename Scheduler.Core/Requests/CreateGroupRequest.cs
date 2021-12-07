using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class CreateGroupRequest
    {
        [StringLength(10, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
