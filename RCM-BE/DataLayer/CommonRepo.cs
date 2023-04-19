
using DataLayer.Entities;

namespace DataLayer
{
    public class CommonRepo
    {
        private readonly RevenueCycleDbContext _dbContext;

        public CommonRepo(RevenueCycleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<RoleMst> RoleMstList(bool? IsDeleted = false, bool? IsActive = true)
        {
            return _dbContext.RoleMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
        }

        public IQueryable<UserMst> UserMstList(bool? IsDeleted = false, bool? IsActive = true)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
        }

		public IQueryable<AppointmentMst> AppointmentMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.AppointmentMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

		public IQueryable<PatientEmailMst> PatientEmailMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.PatientEmailMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

		public IQueryable<PhysicianMst> PhysicianMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.PhysicianMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

		public IQueryable<NotificationMst> NotificationMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.NotificationMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

		public IQueryable<RemarkMst> RemarkMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.RemarkMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

		public IQueryable<CallTypeMst> CallTypeMstList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.CallTypeMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
		}

	}
}
