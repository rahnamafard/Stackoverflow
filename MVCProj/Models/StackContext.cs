using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProj.Models;

namespace MVCProj.Models
{
    public class StackContext : IdentityDbContext<User>
    {
        public StackContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.Questions);
        }
    }
}