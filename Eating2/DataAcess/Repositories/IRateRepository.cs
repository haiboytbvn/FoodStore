using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eating2.DataAcess.Models;

namespace Eating2.DataAcess.Repositories
{
    public interface IRateRepository
    {
        //IEnumerable<RateDataModel> ListAll();
        RateDataModel GetRateByID(int RateID);
        void InsertRate(RateDataModel Rate);
        void DeleteRate(int RateID);
        void UpdateRate(RateDataModel Rate);
        void Save();
        IEnumerable<RateDataModel> ListAllForFood(int id);
        int TotalRate(int FoodId);
    }
}
