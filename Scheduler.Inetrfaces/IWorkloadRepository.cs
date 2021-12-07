using Scheduler.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface IWorkloadRepository
    {
        Task<IEnumerable<Workload>> GetWorkloads();
        Task<Workload> GetWorkloadById(int id);
        Task AddWorkload(Workload workload);
        Task<int> WorkloadsCount();
        Task<IEnumerable<Workload>> TakeWorkloads(int count, int offset);
        Task UpdateWorkload(Workload workload);
        Task RemoveWorkload(Workload workload);
    }
}
