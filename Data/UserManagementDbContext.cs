using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Models;

namespace SampleUserManagement.Data
{
    public class UserManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UserManagementDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("3B5BAAF6-0921-4CF2-3EE8-08DD913EFF83"),
                    UserId = "UID00001",
                    FirstName = "Chris",
                    LastName = "Ng",
                    Email = "chisxuan777@gmail.com",
                    Status = 1,
                    CreatedAt = new DateTime(2025, 5, 9),
                },
                new User
                {
                    Id = Guid.Parse("5DB0FCA6-4387-4D01-3EE9-08DD913EFF83"),
                    UserId = "UID00002",
                    FirstName = "Xavier",
                    LastName = "Lim",
                    Email = "xav1002@gmail.com",
                    Status = 1,
                    CreatedAt = new DateTime(2025, 5, 10),
                },
                new User
                {
                    Id = Guid.Parse("B120D162-ECE4-4290-3EEA-08DD913EFF83"),
                    UserId = "UID00003",
                    FirstName = "Joey",
                    LastName = "Lim",
                    Email = "joeylim002@gmail.com",
                    Status = 1,
                    CreatedAt = new DateTime(2025, 5, 11),
                }
            );
        }
    }
}
