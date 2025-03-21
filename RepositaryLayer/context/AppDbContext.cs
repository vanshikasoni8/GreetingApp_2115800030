using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modellayer.Model;
using RepositaryLayer.Entity;

namespace Modellayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=VANSHIKA\\SQLEXPRESS;Initial Catalog=GreetingNew;Integrated Security=TRUE;",
                    sqlOptions => sqlOptions.EnableRetryOnFailure());
            }
        }

        public DbSet<GreetingEntity> Greetings { get; set; }

        public DbSet<UserEntity> User{ get; set; }


    }
}
