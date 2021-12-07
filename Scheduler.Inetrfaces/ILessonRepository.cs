using Scheduler.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface ILessonRepository
    {
        Task AddLesson(Lesson lesson);
        Task<IEnumerable<Lesson>> GetLessons(DateTime begin, DateTime end);
        Task<IEnumerable<Lesson>> LessonsForStudent(Student student);
        Task<IEnumerable<Lesson>> LessonsForStudent(Student student, DateTime begin, DateTime end);
    }
}
