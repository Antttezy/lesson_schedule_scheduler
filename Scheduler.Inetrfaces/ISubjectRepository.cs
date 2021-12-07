using Scheduler.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetSubjects();
        Task AddSubject(Subject subject);
        Task UpdateSubject(Subject subject);
        Task<IEnumerable<Subject>> TakeSubjects(int count, int offset);
        Task<int> SubjectsCount();
    }
}
