namespace Eating2.DataAcess.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Eating2.DataAcess.Repositories;

    internal sealed class Configuration : DbMigrationsConfiguration<Eating2.DataAcess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Eating2.DataAcess.ApplicationDbContext context)
        {
           
            //var RateRepository = new RateRepository();
            //var FoodRepository = new FoodRepository();
            //var foods = context.Foods.Select(t => t);
            //foreach(var item in foods)
            //{
            //    item.numberOfComment = RateRepository.TotalRate(item.ID);
            //    item.Created = DateTime.Now;
               
            //}

            //context.SaveChanges();
           
        }
    }
}
