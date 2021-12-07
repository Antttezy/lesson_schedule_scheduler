using Microsoft.EntityFrameworkCore;
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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext context;

        public SubjectRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddSubject(Subject subject)
        {
            try
            {
                await context.Subjects.AddAsync(subject);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new RepositoryException();
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await context.Subjects.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<IEnumerable<Subject>> TakeSubjects(int count, int offset)
        {
            return await context.Subjects
                .OrderBy(s => s.Name)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> SubjectsCount()
        {
            return await context.Subjects.CountAsync();
        }

        public async Task UpdateSubject(Subject subject)
        {
            try
            {
                context.Subjects.Update(subject);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new RepositoryException();
            }
        }
    }
}
