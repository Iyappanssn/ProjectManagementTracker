
using Microsoft.EntityFrameworkCore;
using ProjectManagementTracker.Models;

namespace ProjectManagementTracker
{
    public class MyDataContext: DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options):base(options)
        {

        }

        public DbSet<MemberDetails> MemberDetails { get; set; }
        public DbSet<TaskDetails> TaskDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberDetails>().ToTable("MemberDetails");
            modelBuilder.Entity<TaskDetails>().ToTable("TaskDetails");
        }

    }
}
