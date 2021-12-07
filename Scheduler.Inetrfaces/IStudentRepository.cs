using Scheduler.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Inetrfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<IEnumerable<Student>> FindStudents(int count, int offset);
        Task<IEnumerable<Student>> FindStudents(int count, int offset, string searchPattern);
        Task AddStudent(Student student);
        Task UpdateStudent(Student student);
        Task RemoveStudent(Student student);

        Task<int> StudentCount();
    }
}
