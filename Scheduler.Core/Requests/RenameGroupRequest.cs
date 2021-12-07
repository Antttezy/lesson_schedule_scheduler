using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Requests
{
    public class RenameGroupRequest
    {
        [Required]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
