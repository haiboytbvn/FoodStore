using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eating2.DataAcess.Models;

namespace Eating2.DataAcess.Repositories
{
    public interface IStoreRepository
    {
        IEnumerable<StoreDataModel> ListAll();
        StoreDataModel GetStoreByID(int StoreID);
        void InsertStore(StoreDataModel Store);
        void DeleteStore(int StoreID);
        void UpdateStore(StoreDataModel Store);
        void Save();
        IEnumerable<StoreDataModel> ListAllForOwner(string id);
        int TotalStore(string UserID);


    }
}
