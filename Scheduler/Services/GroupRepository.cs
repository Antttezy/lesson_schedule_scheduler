using Scheduler.Context;
using Scheduler.Core.Exceptions;
using Scheduler.Data;
using Scheduler.Inetrfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext context;

        public GroupRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddGroup(Group group)
        {
            await context.Groups.AddAsync(group);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new RepositoryException();
            }
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await context.Groups.Include(g => g.Workloads).ThenInclude(w => w.Subject).Include(g => g.Workloads).ThenInclude(w => w.Subject).Include(g => g.Students).ThenInclude(s => s.Group).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await context.Groups.Include(g => g.Students).ThenInclude(s => s.Group).ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsSorted()
        {
            return await context.Groups.Include(g => g.Students).ThenInclude(s => s.Group).OrderBy(g => g.Name).ToListAsync();
        }

        public async Task UpdateGroup(Group group)
        {
            context.Groups.Update(group);
            await context.SaveChangesAsync();
        }
    }
}
