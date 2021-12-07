using Scheduler.Data;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher> GetTeacherById(int id);
    }
}
