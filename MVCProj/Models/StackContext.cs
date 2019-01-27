using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class StackContext : IdentityDbContext<User>
    {
        public StackContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}