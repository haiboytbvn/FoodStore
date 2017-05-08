using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using System.Data.Entity;
using PagedList;
using Eating2.AppConfig;

namespace Eating2.DataAcess.Repositories
{
    public class FoodRepository : RepositoryBase, IFoodRepository
    {
        public void DeleteFood(int FoodID)
        {
            FoodDataModel Food = dataContext.Foods.Find(FoodID);
            dataContext.Foods.Remove(Food);
        }

        public FoodDataModel GetFoodByID(int FoodID)
        {
            return dataContext.Foods.Find(FoodID);
        }

        public void InsertFood(FoodDataModel Food)
        {
            dataContext.Foods.Add(Food);
        }

        public IEnumerable<FoodDataModel> ListAll()
        {
            return dataContext.Foods.ToList();
        }

        public IEnumerable<FoodDataModel> ListAllForStore(int id)
        {
            return dataContext.Foods.Where(t => t.StoreID == id).Select(t => t).ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public int TotalFood(int StoreId)
        {
            return dataContext.Foods.Where(f => f.StoreID == StoreId).Count();
        }

        public void UpdateFood(FoodDataModel Food)
        {
            dataContext.Entry(Food).State = EntityState.Modified;
        }

        public IPagedList<FoodDataModel> GetFoodsForSearch(FilterOptions filterOptions)
        {
            IQueryable<FoodDataModel> query = dataContext.Foods.Select(t => t).OrderBy(t => t.Name);
            if (filterOptions == null)
            {
                return query.ToCustomPagedList<FoodDataModel>(0, 12);
            }
            if (!string.IsNullOrEmpty(filterOptions.Keyword))
            {
                foreach (var field in filterOptions.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "storenamedisplayonly":
                            query = query.Where(t => t.Name.Contains(filterOptions.Keyword));
                            break;
                        case "name":
                            query = query.Where(t => t.Name.Contains(filterOptions.Keyword));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (filterOptions.SortOptions != null)
            {
                var sortOptions = filterOptions.SortOptions;
                switch (sortOptions.Field.ToLowerInvariant())
                {
                    case "name":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                        break;
                    case "cost":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Cost) : query.OrderByDescending(t => t.Cost);
                        break;
                    case "storenamedisplayonly":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.StoreNameDisplayOnly) : query.OrderByDescending(t => t.StoreNameDisplayOnly);
                        break;
                    case "districtdisplayonly":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.DistrictDisplayOnly) : query.OrderByDescending(t => t.DistrictDisplayOnly);
                        break;
                    case "averagepoint":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.AveragePoint) : query.OrderByDescending(t => t.AveragePoint);
                        break;

                }
                
            }

            if (filterOptions.PagingOptions != null)
            {
                var pagingOption = filterOptions.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(0, 12);
        }

    }
}