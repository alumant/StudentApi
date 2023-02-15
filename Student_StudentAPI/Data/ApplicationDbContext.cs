using Microsoft.EntityFrameworkCore;
using Student_StudentAPI.Models;

namespace Student_StudentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> students { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}
