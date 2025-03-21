using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modellayer.Model;
using RepositaryLayer.Entity;
using RepositaryLayer.Service;

namespace Modellayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //            "Data Source=VANSHIKA\\SQLEXPRESS;Initial Catalog=GreetingNew;Integrated Security=TRUE;",
        //            sqlOptions => sqlOptions.EnableRetryOnFailure());
        //    }
        //}

        public DbSet<GreetingEntity> Greetings { get; set; }

        public DbSet<UserEntity> User{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GreetingEntity>()
                .HasOne(g => g.User)  // Greeting has one User
                .WithMany(u => u.Greeting)  // User has many Greetings
                .HasForeignKey(g => g.UserId)  // Foreign Key in Greeting
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete

            base.OnModelCreating(modelBuilder);
        }

    }
}
