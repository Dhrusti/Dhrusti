using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace Helper
{
	public class CommonRepo
	{
		private readonly Erp1OdbContext _dbContext;

		public CommonRepo(Erp1OdbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<UserMst> userList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
	}
}
