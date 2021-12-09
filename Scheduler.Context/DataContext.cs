using Scheduler.Data;
using System.Diagnostics.CodeAnalysis;

namespace Scheduler.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Workload> Workloads { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public DataContext([NotNull] DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
