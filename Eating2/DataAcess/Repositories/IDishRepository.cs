using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eating2.DataAcess.Models;

namespace Eating2.DataAcess.Repositories
{
    public interface IDishRepository
    {
        IEnumerable<DishDataModel> ListAll();
        DishDataModel GetDishByID(int DishID);
        void InsertDish(DishDataModel Dish);
        void DeleteDish(int DishID);
        void UpdateDish(DishDataModel Dish);
        void Save();
        IEnumerable<DishDataModel> ListAllForOwner(string id);
        int TotalDish(string UserID);


    }
}
