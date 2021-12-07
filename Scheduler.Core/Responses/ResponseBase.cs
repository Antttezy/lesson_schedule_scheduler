using System.Collections.Generic;

namespace Scheduler.Core.Responses
{
    public class ResponseBase
    {
        public bool IsOk { get; set; }

        public ICollection<string> Errors { get; }

        public ResponseBase()
        {
            Errors = new List<string>();
        }

        public ResponseBase(params string[] errors)
        {
            IsOk = false;
            Errors = new List<string>(errors);
        }
    }
}
