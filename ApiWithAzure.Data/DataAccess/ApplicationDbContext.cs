
using ApiWithAzure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithAzure.Data.Model
{
   public class ApplicationDbContext: IdentityDbContext<ApplicationUser,IdentityRole,string>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Member>().HasIndex(room => room.Email).IsUnique();
            
        }

        public DbSet<Member> Members { get; set; }
    }
}
