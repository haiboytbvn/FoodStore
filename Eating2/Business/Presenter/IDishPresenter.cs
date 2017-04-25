using Eating2.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.Business.Presenter
{
    public interface IDishPresenter
    {
        DishViewModel GetDishById(int DishID);
        List<DishViewModel> ListAllDish();
        List<DishViewModel> ListAllDishForOwner(string id);
        void InsertDish(DishViewModel Dish);
        void DeleteDish(int DishID);
        void UpdateDish(int DishID, DishViewModel Dish);
        string GetDishPictureUrlForUpload(int DishID, string UserName);
        string GetDishDirectionPicture(int DishID, int number, string UserName);
    }
}
