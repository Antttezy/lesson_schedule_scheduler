using Microsoft.EntityFrameworkCore;
using Scheduler.Context;
using Scheduler.Data;
using Scheduler.Inetrfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddStudent(Student student)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> FindStudents(int count, int offset)
        {
            return await context.Students.Include(s => s.Group)
                .OrderBy(s => s.SecondName)
                .ThenBy(s => s.FirstName)
                .Skip(offset).Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> FindStudents(int count, int offset, string searchPattern)
        {
            return await context.Students.Include(s => s.Group)
                .Where(st => st.SecondName.Contains(searchPattern))
                .OrderBy(s => s.SecondName)
                .ThenBy(s => s.FirstName)
                .Skip(offset).Take(count)
                .ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await context.Students.Include(s => s.Group).ThenInclude(g => g.Students).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<IEnumerable<Student>> GetStudents()
        {
            return Task.FromResult(context.Students.Include(s => s.Group).AsEnumerable());
        }

        public async Task RemoveStudent(Student student)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }

        public async Task<int> StudentCount()
        {
            return await context.Students.CountAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
    }
}
