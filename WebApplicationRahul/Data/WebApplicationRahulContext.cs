using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationRahul.Models;

namespace WebApplicationRahul.Data
{
    public class WebApplicationRahulContext : DbContext
    {
        public WebApplicationRahulContext (DbContextOptions<WebApplicationRahulContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationRahul.Models.User> User { get; set; } = default!;

        public DbSet<WebApplicationRahul.Models.Link> Links { get; set; } = default!;   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Links)
                                        .WithOne(l => l.User)
                                        .HasForeignKey(l => l.UserId);
        }
    }
}
