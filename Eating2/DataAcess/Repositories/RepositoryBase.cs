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
    }
}