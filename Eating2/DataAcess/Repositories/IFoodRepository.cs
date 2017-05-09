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
        int TotalFood(int StoreId);
        IPagedList<FoodDataModel> GetFoodsForSearch(FilterOptions filterOptions);
        IPagedList<FoodDataModel> ListAllFoodByRate(FilterOptions filterOptions);
        IPagedList<FoodDataModel> ListAllFoodByArea(string district, FilterOptions filterOptions);
        IPagedList<FoodDataModel> ListAllFoodByTime(FilterOptions filterOptions);
        IEnumerable<FoodDataModel> ListByRate();
        IEnumerable<FoodDataModel> ListByRateIndex();
        List<FoodDataModel> ListByArea(string district);
        List<FoodDataModel> ListByAreaIndex(string district);
        IEnumerable<FoodDataModel> ListByTime();
        IEnumerable<FoodDataModel> ListByTimeIndex();
        List<FoodDataModel> ListByCostRecomment(double cost);

    }
}
