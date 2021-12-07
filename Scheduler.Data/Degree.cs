using System.ComponentModel.DataAnnotations;

namespace Scheduler.Data
{
    public enum Degree
    {
        [Display(Name = "Бакалавр")]
        Bachelor,

        [Display(Name = "Магистр")]
        Master,

        [Display(Name = "Доктор")]
        Doctor
    }
}
