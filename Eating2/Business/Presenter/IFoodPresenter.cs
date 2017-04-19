using Eating2.Business.ViewModels;
using Eating2.DataAcess.Models;
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
        void InsertFood(FoodViewModel Food);
        void DeleteFood(int FoodID);
        void UpdateFood(int FoodID, FoodViewModel Food);
        string GetFoodPictureUrlForUpload(string FoodName, int number, string StoreName, string UserName);
    }
}
