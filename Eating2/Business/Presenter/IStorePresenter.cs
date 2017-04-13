using Eating2.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.Business.Presenter
{
    public interface IStorePresenter
    {
        StoreViewModel GetStoreById(int StoreID);
        List<StoreViewModel> ListAllStore();
        List<StoreViewModel> ListAllStoreForOwner(string id);
        void InsertStore(StoreViewModel store);
        void DeleteStore(int StoreID);
        void UpdateStore(int storeID, StoreViewModel store);
    }
}
