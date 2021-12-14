using Microsoft.EntityFrameworkCore;
using Scheduler.Context;
using Scheduler.Data;
using Scheduler.Inetrfaces;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext context;

        public TeacherRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
