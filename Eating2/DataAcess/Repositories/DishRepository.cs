using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using System.Data.Entity;

namespace Eating2.DataAcess.Repositories
{
    public class DishRepository : RepositoryBase, IDishRepository
    {
        public void DeleteDish(int DishID)
        {
            DishDataModel Dish = dataContext.Dishes.Find(DishID);
            dataContext.Dishes.Remove(Dish);
        }

        public DishDataModel GetDishByID(int DishID)
        {
            return dataContext.Dishes.Find(DishID);
        }

        public void InsertDish(DishDataModel Dish)
        {
            dataContext.Dishes.Add(Dish);
        }

        public IEnumerable<DishDataModel> ListAll()
        {
            return dataContext.Dishes.ToList();
        }

        public IEnumerable<DishDataModel> ListAllForOwner(string id)
        {
            return dataContext.Dishes.Where(t => t.Owner == id).Select(t => t).ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public void UpdateDish(DishDataModel Dish)
        {
            dataContext.Entry(Dish).State = EntityState.Modified;
        }

        public int TotalDish(string UserID)
        {
            return dataContext.Dishes.Where(d => d.Owner == UserID).Count();
        }
    }
}