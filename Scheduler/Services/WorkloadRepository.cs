using Microsoft.EntityFrameworkCore;
using Scheduler.Context;
using Scheduler.Data;
using Scheduler.Inetrfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class WorkloadRepository : IWorkloadRepository
    {
        private readonly DataContext context;

        public WorkloadRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddWorkload(Workload workload)
        {
            await context.Workloads.AddAsync(workload);
            await context.Entry(workload).Reference<Subject>("Subject").LoadAsync();
            await context.SaveChangesAsync();
        }

        public async Task<Workload> GetWorkloadById(int id)
        {
            return await context.Workloads.Include(w => w.Subject).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Workload>> GetWorkloads()
        {
            return await context.Workloads.Include(w => w.Subject)
                .OrderBy(w => w.Subject.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Workload>> TakeWorkloads(int count, int offset)
        {
            return await context.Workloads.Include(w => w.Subject)
                .OrderBy(w => w.Subject.Name)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> WorkloadsCount()
        {
            return await context.Workloads.CountAsync();
        }

        public async Task RemoveWorkload(Workload workload)
        {
            context.Workloads.Remove(workload);
            await context.SaveChangesAsync();
        }

        public async Task UpdateWorkload(Workload workload)
        {
            context.Workloads.Update(workload);
            await context.SaveChangesAsync();
        }
    }
}
