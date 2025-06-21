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

        public DbSet<Course> Cursos => Set<Course>();
        public DbSet<Grade> Grados => Set<Grade>(); 
    }
}
