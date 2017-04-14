using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using System.Data.Entity;

namespace Eating2.DataAcess.Repositories
{
    public class RateRepository : RepositoryBase, IRateRepository
    {
        public void DeleteRate(int RateID)
        {
            RateDataModel Rate = dataContext.Rates.Find(RateID);
            dataContext.Rates.Remove(Rate);
        }

        public RateDataModel GetRateByID(int RateID)
        {
            return dataContext.Rates.Find(RateID);
        }

        public void InsertRate(RateDataModel Rate)
        {
            dataContext.Rates.Add(Rate);
        }

        //public IEnumerable<RateDataModel> ListAll()
        //{
        //    return dataContext.Rates.ToList();
        //}

        public IEnumerable<RateDataModel> ListAllForFood(int id)
        {
            return dataContext.Rates.Where(t => t.FoodID == id).Select(t => t).ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public void UpdateRate(RateDataModel Rate)
        {
            dataContext.Entry(Rate).State = EntityState.Modified;
        }
    }
}