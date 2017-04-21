using Eating2.DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.DataAcess.Repositories
{
    public class RepositoryBase
    {
        protected ApplicationDbContext dataContext;

        public RepositoryBase()
        {
            dataContext = new ApplicationDbContext();
        }

        public virtual IQueryable<FoodDataModel> OrderByID(IQueryable<FoodDataModel> query)
        {
            query = (query as IOrderedQueryable<FoodDataModel>).ThenByDescending(t => t.ID);
            return query;
        }

        public virtual IQueryable<StoreDataModel> OrderByID(IQueryable<StoreDataModel> query)
        {
            query = (query as IOrderedQueryable<StoreDataModel>).ThenByDescending(t => t.ID);
            return query;
        }
    }
}