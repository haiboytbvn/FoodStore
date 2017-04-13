using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using System.Data.Entity;

namespace Eating2.DataAcess.Repositories
{
    public class StoreRepository : RepositoryBase, IStoreRepository
    {
        public void DeleteStore(int StoreID)
        {
            StoreDataModel store = dataContext.Stores.Find(StoreID);
            dataContext.Stores.Remove(store);
        }

        public StoreDataModel GetStoreByID(int StoreID)
        {
            return dataContext.Stores.Find(StoreID);
        }

        public void InsertStore(StoreDataModel Store)
        {
            dataContext.Stores.Add(Store);
        }

        public IEnumerable<StoreDataModel> ListAll()
        {
            return dataContext.Stores.ToList();
        }

        public IEnumerable<StoreDataModel> ListAllForOwner(string id)
        {
            return dataContext.Stores.Where(t => t.Owner == id).Select(t => t).ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public void UpdateStore(StoreDataModel Store)
        {
            dataContext.Entry(Store).State = EntityState.Modified;
        }
    }
}