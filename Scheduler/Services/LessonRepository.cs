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
    public class LessonRepository : ILessonRepository
    {
        private readonly DataContext context;

        public LessonRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddLesson(Lesson lesson)
        {
            try
            {
                await context.Lessons.AddAsync(lesson);
                await context.SaveChangesAsync();
                await context.Entry(lesson).Reference<Workload>("Workload").LoadAsync();
                await context.Entry(lesson).Reference<Group>("Group").LoadAsync();
                await context.Entry(lesson).Reference<Teacher>("Teacher").LoadAsync();
                await context.Entry(lesson.Workload).Reference<Subject>("Subject").LoadAsync();
            }
            catch (Exception)
            {
                throw new RepositoryException();
            }
        }

        public async Task<IEnumerable<Lesson>> GetLessons(DateTime begin, DateTime end)
        {
            return await context.Lessons.Include(l => l.Teacher).Include(l => l.Workload).ThenInclude(w => w.Subject).Include(l => l.Group)
                .Where(l => l.LessonTime >= begin && l.LessonTime <= end)
                .OrderBy(l => l.LessonTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> LessonsForStudent(Student student)
        {
            await context.Entry(student).ReloadAsync();

            return await context.Lessons.Include(l => l.Teacher).Include(l => l.Workload).ThenInclude(w => w.Subject).Include(l => l.Group)
                .Where(lesson => lesson.GroupId == student.GroupId)
                .OrderBy(l => l.LessonTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> LessonsForStudent(Student student, DateTime begin, DateTime end)
        {
            await context.Entry(student).ReloadAsync();

            return await context.Lessons.Include(l => l.Teacher).Include(l => l.Workload).ThenInclude(w => w.Subject).Include(l => l.Group)
                .Where(lesson => lesson.GroupId == student.GroupId)
                .Where(lesson => lesson.LessonTime >= begin && lesson.LessonTime <= end)
                .OrderBy(l => l.LessonTime)
                .ToListAsync();
        }
    }
}
