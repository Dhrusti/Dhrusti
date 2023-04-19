using DataLayer.Entities;

namespace Helper
{
    public class CommonRepo
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly AuthRepo _authRepo;

        public CommonRepo(WaltCapitalDBContext dbContext, AuthRepo authRepo)
        {
            _dbContext = dbContext;
            _authRepo = authRepo;
        }

        public IQueryable<OfficeMst> officeList(bool IsDeleted = false, bool IsActive = true)
        {
            return (IQueryable<OfficeMst>)_authRepo.GetRoleBasedData(_dbContext.OfficeMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable(), "", true);
        }

        public IQueryable<PersonalityTypeMst> personalityType(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.PersonalityTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<AccountTypeMst> accountTypeList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.AccountTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<WaltCapConsultantMst> waltCapConsultantList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.WaltCapConsultantMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<AccessCategoryTypeMst> accessCategoryTypeList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.AccessCategoryTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<AccessCategoryMst> accessCategoryList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.AccessCategoryMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<UserMst> ALLUserList(bool isValidate = true)
        {
            return (IQueryable<UserMst>)_authRepo.GetRoleBasedData(_dbContext.UserMsts.AsQueryable(), "", isValidate);
        }

        public IQueryable<ExternalAccountDetail> externalAccountList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ExternalAccountDetails.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<ClientTypeMst> clientTypeList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ClientTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<ServiceProviderMst> serviceProviderList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ServiceProviderMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<ServiceProviderTypeMst> serviceProviderTypeList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ServiceProviderTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<CountryCustomMst> countryCustomList()
        {
            return (IQueryable<CountryCustomMst>)_authRepo.GetRoleBasedData(_dbContext.CountryCustomMsts.AsQueryable(), "", true);
        }

        public IQueryable<StateCustomMst> stateCustomList()
        {
            return (IQueryable<StateCustomMst>)_authRepo.GetRoleBasedData(_dbContext.StateCustomMsts.AsQueryable(), "", true);
        }

        public IQueryable<CityCustomMst> cityCustomList()
        {
            return (IQueryable<CityCustomMst>)_authRepo.GetRoleBasedData(_dbContext.CityCustomMsts.AsQueryable(), "", true);
        }

        public IQueryable<AccessCategoryPermissionMst> accessCategoryPermissionMsts(bool IsDeleted = false, bool IsActive = true)
        {
            IQueryable<AccessCategoryPermissionMst> List = _dbContext.AccessCategoryPermissionMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
            return List;
        }

        public IQueryable<ClientTransactionMst> clientTransactionList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.ClientTransactionMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }


        public IQueryable<FundMst> fundList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.FundMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable();
        }

        public IQueryable<PermissionCredentialMst> permissionCredentialList()
        {
            return _dbContext.PermissionCredentialMsts.AsQueryable();


        }

        public IQueryable<FundDynamicFieldMst> fundDynamicFieldList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.FundDynamicFieldMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<FactSheetMst> factSheetList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.FactSheetMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<UserMst> getUserList(bool IsDeleted = false, bool IsActive = true)
        {
            //return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
            return (IQueryable<UserMst>)_authRepo.GetRoleBasedData(_dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable(), "Id");
        }

        public IQueryable<UserMst> getUserList_Login(bool IsDeleted = false)
        {
            return (IQueryable<UserMst>)_authRepo.GetRoleBasedData(_dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted).AsQueryable(), "", false);
        }

        public IQueryable<DueDiligenceMst> dueDiligenceList()
        {
            return _dbContext.DueDiligenceMsts.AsQueryable();
        }

        public IQueryable<Amlmst> amlList()
        {
            return _dbContext.Amlmsts.AsQueryable();
        }

        public IQueryable<CsvfileUploadLogMst> getPPMCSVLogList()
        {
            return _dbContext.CsvfileUploadLogMsts.AsQueryable();
        }

        public IQueryable<CsvdataMst> getPPMCSVDataList()
        {
            return _dbContext.CsvdataMsts.AsQueryable();
        }

        public IQueryable<UserMst> getIFAClientList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<UserDocumentMst> getUserDocumentList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.UserDocumentMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<RoleMst> getRoleList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.RoleMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }

        public IQueryable<RoleMst> getAllRoleList()
        {
            return _dbContext.RoleMsts.AsQueryable();
        }

        public IQueryable<CurrencyMst> currencyList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.CurrencyMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
        }
        public IQueryable<UserMst> getUserRoleList(bool IsDeleted = false, bool IsActive = true, string role = null)
        {
            return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive && x.Role == role).AsQueryable();
        }

        public IQueryable<FundDynamicInputPriceMst> GetFundDynamicInputPriceList(int FundId)
        {
            return _dbContext.FundDynamicInputPriceMsts.Where(x => x.FundId == FundId && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        public IQueryable<PricingMst> GetPricingList(int FundId)
        {
            return _dbContext.PricingMsts.Where(x => x.FundId == FundId && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        //public IQueryable<FundCurrentBalanceMst> GetFundCurrentBalanceList(int FundId, DateTime BalanceDate)
        //{
        //    return _dbContext.FundCurrentBalanceMsts.Where(x => x.FundId == FundId && x.BalanceDate.Date == BalanceDate.Date && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        //}
        public IQueryable<PricingMst> GetAllPricingList()
        {
            return _dbContext.PricingMsts.Where(x => x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        public IQueryable<FundBenchMarkMst> getAllFundBenchMarkList(bool IsDeleted = false, bool IsActive = true)
        {
            return _dbContext.FundBenchMarkMsts.Where(x => x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        public IQueryable<FundBenchMarkMst> FundBenchMarkList()
        {
            return _dbContext.FundBenchMarkMsts.AsQueryable();
        }

        public IQueryable<MeetingMst> meetingList(bool IsDeleted = false, bool IsActive = true, string role = null)
        {
            return _dbContext.MeetingMsts.Where(x => x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        public IQueryable<FundFeeCalculationDetail> FundFeeCalculationList(int FundId, DateTime BalanceDate)
        {
            return _dbContext.FundFeeCalculationDetails.Where(x => x.FundId == FundId && x.BalanceDate.Date == BalanceDate.Date && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }

        public IQueryable<FundFeeCalculationDetail> GetFundFeeCalculationList(int FundId)
        {
            return _dbContext.FundFeeCalculationDetails.Where(x => x.FundId == FundId && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }
        public IQueryable<LinkMst> linkList(bool IsDeleted = false, bool IsActive = true, string role = null)
        {
            return _dbContext.LinkMsts.AsQueryable();
        }

		public IQueryable<FundFeesMst> GetFundFeeList(int FundId)
        {
            return _dbContext.FundFeesMsts.Where(x => x.FundId == FundId && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }

        public IQueryable<RunFeesDetail> GetRunFeesDetailList(int FundId)
        {
            return _dbContext.RunFeesDetails.Where(x => x.FundId == FundId && x.IsDeleted == false && x.IsActive == true).AsQueryable();
        }

        public IQueryable<RunFeesDetail> GetAllRunFeesDetailList(int FundId)
        {
            return _dbContext.RunFeesDetails.Where(x => x.FundId == FundId).AsQueryable();
        }
    }
}
