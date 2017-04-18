using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using System.Data.Entity;

namespace Eating2.DataAcess.Repositories
{
    public class FoodRepository : RepositoryBase, IFoodRepository
    {
        public void DeleteFood(int FoodID)
        {
            FoodDataModel Food = dataContext.Foods.Find(FoodID);
            dataContext.Foods.Remove(Food);
        }

        public FoodDataModel GetFoodByID(int FoodID)
        {
            return dataContext.Foods.Find(FoodID);
        }

        public void InsertFood(FoodDataModel Food)
        {
            dataContext.Foods.Add(Food);
        }

        public IEnumerable<FoodDataModel> ListAll()
        {
            return dataContext.Foods.ToList();
        }

        public IEnumerable<FoodDataModel> ListAllForStore(int id)
        {
            return dataContext.Foods.Where(t => t.StoreID == id).Select(t => t).ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public int TotalFood(int StoreId)
        {
            return dataContext.Foods.Where(f => f.StoreID == StoreId).Count();
        }

        public void UpdateFood(FoodDataModel Food)
        {
            dataContext.Entry(Food).State = EntityState.Modified;
        }
    }
}