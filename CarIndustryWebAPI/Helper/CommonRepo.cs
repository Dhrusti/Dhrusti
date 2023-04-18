using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace Helper
{
    public  class CommonRepo
    {
        public readonly CarIndustryDBContext _dbContext;

        public CommonRepo(CarIndustryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CarMst> carList(bool IsDeleted = false, bool IsActive = true)
        {
            List<CarMst> List = new List<CarMst>();
            List = _dbContext.CarMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).ToList();
            return List;
        }

        public List<BrandMst> brandList()
        {
            List<BrandMst> List = new List<BrandMst>();
            List = _dbContext.BrandMsts.Where(x => x.Id <= 5).ToList();
            return List;
        }

    }
}
