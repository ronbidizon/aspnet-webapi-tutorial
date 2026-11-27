using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDB_context : IdentityDbContext<api.Models.AppUser>
    {
        public ApplicationDB_context(DbContextOptions<ApplicationDB_context> options) : base(options)
        {
        }

        public DbSet<api.Models.Stock> Stocks { get; set; }
        public DbSet<api.Models.Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "Admin"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "User"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}