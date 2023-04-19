using DataLayer.Entities;

namespace Helper
{
	public class CommonRepo
	{
		private readonly ErpDbContext _dbContext;

		public CommonRepo(ErpDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<UserMst> userList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<RegistrationMst> registrations(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.RegistrationMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<GenderMst> gender(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.GenderMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<EmployementTypeMst> employementTypeList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.EmployementTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<CompanyMst> companyList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.CompanyMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<RoleMst> roleList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.RoleMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<ReportingManagerMst> reportingManagerList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.ReportingManagerMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<DesignationMst> designationList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.DesignationMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<RequirementMst> requirements(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.RequirementMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
	}
}






















































































