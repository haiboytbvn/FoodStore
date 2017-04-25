using Eating2.AppConfig;
using Eating2.Business.ViewModels;
using Eating2.DataAcess.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.Business.Presenter
{
    public interface IFoodPresenter
    {
        FoodViewModel GetFoodById(int FoodID);
        List<FoodViewModel> ListAllFood();
        List<FoodViewModel> ListAllFoodForStore(int Id);
        List<FoodViewModel> ListAllFoodForDish(int Id);
        void InsertFood(FoodViewModel Food);
        void DeleteFood(int FoodID);
        void UpdateFood(int FoodID, FoodViewModel Food);
        string GetFoodDirectionPicture(int foodID, int storeID, string UserName);
        string GetFoodPictureUrlForUpload(int foodID, int number, int storeID, string UserName);
        IPagedList<FoodViewModel> GetFoodsForSearch(FilterOptions filterOptions);

    }
}
