using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eating2.DataAcess.Models;
using PagedList;
using Eating2.AppConfig;

namespace Eating2.DataAcess.Repositories
{
    public interface IFoodRepository
    {
        IEnumerable<FoodDataModel> ListAll();
        FoodDataModel GetFoodByID(int FoodID);
        void InsertFood(FoodDataModel Food);
        void DeleteFood(int FoodID);
        void UpdateFood(FoodDataModel Food);
        void Save();
        IEnumerable<FoodDataModel> ListAllForStore(int id);
        IEnumerable<FoodDataModel> ListAllForDish(int id);
        int TotalFood(int StoreId);
        int TotalStoreHasFood(int DishId);
        IPagedList<FoodDataModel> GetFoodsForSearch(FilterOptions filterOptions);
    }
}
