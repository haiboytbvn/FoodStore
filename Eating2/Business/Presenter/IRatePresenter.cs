using Eating2.Business.ViewModels;
using Eating2.DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating2.Business.Presenter
{
    public interface IRatePresenter
    {
        RateViewModel GetRateById(int RateID);
        //List<RateViewModel> ListAllRate();
        List<RateViewModel> ListAllRateForFood(int Id);
        void InsertRate(RateViewModel Rate);
        void DeleteRate(int RateID);
        //void UpdateRate(int RateID, RateViewModel Rate);
    }
}
