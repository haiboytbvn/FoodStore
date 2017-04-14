using Eating2.DataAcess.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Eating2.DataAcess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<StoreDataModel> Stores { get; set; }
        public DbSet<FoodDataModel> Foods { get; set; }
        public DbSet<CategoryDataModel> Categories { get; set; }
        public DbSet<RateDataModel> Rates { get; set; }

        //Tu dinh nghia khoa ngoai cua cac bang
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Dinh nghia khoa ngoai cua bang Store la ApplicationUser
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreDataModel>()
                .HasKey(t => t.ID)
                .HasRequired(t => t.ApplicationUser)
                .WithMany(t => t.Stores)
                .HasForeignKey(t => t.Owner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodDataModel>()
                 .HasKey(t => t.ID)
                 .HasRequired(t => t.Store)
                 .WithMany(t => t.Foods)
                 .HasForeignKey(t => t.StoreID)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryDataModel>()
                 .HasKey(t => t.ID);


           modelBuilder.Entity<RateDataModel>()
                 .HasKey(t => t.ID)
                 .HasRequired(t => t.Food)
                 .WithMany(t => t.Rates)
                 .HasForeignKey(t => t.FoodID)
                 .WillCascadeOnDelete(false);

        }
    }
}