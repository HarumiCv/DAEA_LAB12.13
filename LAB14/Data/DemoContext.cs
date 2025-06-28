using LAB14.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB14.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options)
            : base (options) 
        {
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Grade> Grades => Set<Grade>(); 
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();  
        public DbSet<Student> Students => Set<Student>();   

    }
}
