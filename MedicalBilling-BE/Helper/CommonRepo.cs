using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CommonRepo
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly AuthRepo _authRepo;

        public CommonRepo(MedicalBillingDbContext dbContext, AuthRepo authRepo)
        {
            _dbContext = dbContext;
            _authRepo = authRepo;
        }


        public IQueryable<UserMst> getUserList_Login(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<ClientMst> getCLientList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ClientMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<PhysicianMst> getAllPhysician(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.PhysicianMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<PhysicianMst> getAllPhysicianList()
        {
            return _dbContext.PhysicianMsts.AsQueryable();
        }
        public IQueryable<UserMst> getPatient(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<AppointmentMst> appointments(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.AppointmentMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<CallTypeMst> getAllCallType(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.CallTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<ExtensionMst> getAllExtension(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ExtensionMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<PhysicianMst> getApptDoctor(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.PhysicianMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<AppointmentMst> getAllAppointment(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.AppointmentMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<AppointmentMst> getAllAppointmentList()
        {
            return _dbContext.AppointmentMsts.AsQueryable();
        }

        public IQueryable<PatientEmailMst> getallpatientnEmail(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.PatientEmailMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<RemarkMst> getallremark(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.RemarkMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<NotificationMst> getAllNotification(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.NotificationMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<UserMst> getAllUsers(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

    }
}
