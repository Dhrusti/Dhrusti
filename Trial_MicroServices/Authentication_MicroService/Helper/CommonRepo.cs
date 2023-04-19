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
		private readonly AuthenticationMicroServiceDbContext _dbContext;
		public CommonRepo(AuthenticationMicroServiceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<RegistrationMst> registrations(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.RegistrationMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

	}
}
