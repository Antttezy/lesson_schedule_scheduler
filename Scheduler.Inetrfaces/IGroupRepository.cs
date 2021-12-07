using Scheduler.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroups();
        Task<IEnumerable<Group>> GetGroupsSorted();
        Task<Group> GetGroupById(int id);
        Task AddGroup(Group group);
        Task UpdateGroup(Group group);
    }
}
