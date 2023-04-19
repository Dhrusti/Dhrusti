
using BusinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace WaltCapitalManagementWebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helpers
            services.AddScoped<CommonRepo>();
            services.AddScoped<CommonHelper>();
            services.AddScoped<AuthRepo>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //BLL
            services.AddScoped<AccessCategoryBLL>();
            services.AddScoped<AccessCategoryTypeBLL>();
            services.AddScoped<AccountTypeBLL>();
            services.AddScoped<AuthBLL>();
            services.AddScoped<ExternalAccountBLL>();
            services.AddScoped<IFABLL>();
            services.AddScoped<OfficeBLL>();
            services.AddScoped<PersonalityTypeBLL>();
            services.AddScoped<RegionBLL>();
            services.AddScoped<UserBLL>();
            services.AddScoped<UserDocumentBLL>();
            services.AddScoped<WaltCapConsultantBLL>();
            services.AddScoped<WatchBLL>();
            services.AddScoped<ClientTypeBLL>();
            services.AddScoped<ServiceProviderBLL>();
            services.AddScoped<ServiceProviderTypeBLL>();
            services.AddScoped<NotificationBLL>();
            services.AddScoped<UploadCSVDocumentBLL>();
            services.AddScoped<UploadClientCSVDocumentBLL>();
            services.AddScoped<ClientTransactionBLL>();
            services.AddScoped<FundBLL>();
            services.AddScoped<DisclaimerBLL>();
            services.AddScoped<PortfolioBLL>();
            services.AddScoped<FactSheetBLL>();
            services.AddScoped<BirthdayReportBLL>();
            services.AddScoped<RoleBLL>();
            services.AddScoped<FundAdministrationClientBLL>();
            services.AddScoped<CurrencyBLL>();
            services.AddScoped<OffshoreBLL>();
            services.AddScoped<FundPricingBLL>();
            services.AddScoped<FundCurrentBalanceBLL>();
            services.AddScoped<FundDynamicInputPriceBLL>();
            services.AddScoped<ClientStatementBLL>();
            services.AddScoped<FundBenchMarkBLL>();
            services.AddScoped<TransactionDetailBLL>();
            services.AddScoped<FundFeeCalculationDetailBLL>();
            services.AddScoped<FeesBLL>();
            services.AddScoped<ReportPortfolioBLL>();
            services.AddScoped<MeetingBLL>();
            services.AddScoped<AllReportsBLL>();
            services.AddScoped<RunFeesBLL>();
            services.AddScoped<FundAdministrationDashBoardBLL>();
            services.AddScoped<DashBoardBLL>();
            services.AddScoped<FundDynamicFieldBLL>();
           

            //Services
            services.AddScoped<IAccessCategory, AccessCategoryImpl>();
            services.AddScoped<IAccessCategoryType, AccessCategoryTypeImpl>();
            services.AddScoped<IAccountType, AccountTypeImpl>();
            services.AddScoped<IAuth, AuthImpl>();
            services.AddScoped<IExternalAccount, ExternalAccountImpl>();
            services.AddScoped<IIFA, IFAImpl>();
            services.AddScoped<IOffice, OfficeImpl>();
            services.AddScoped<IPersonalityType, PersonalityTypeImpl>();
            services.AddScoped<IRegion, RegionImpl>();
            services.AddScoped<IUser, UserImpl>();
            //services.AddScoped<IUserDocument, UploadCSVDocumentImpl>();
            services.AddScoped<IWaltCapConsultant, WaltCapConsultantImpl>();
            services.AddScoped<IWatch, WatchImpl>();
            services.AddScoped<IClientType, ClientImpl>();
            services.AddScoped<IServiceProviderDetails, ServiceProviderImpl>();
            services.AddScoped<IServiceProviderTypeDetail, ServiceProviderTypeImpl>();
            services.AddScoped<INotification, NotificationImpl>();
            services.AddScoped<IUploadCSVDocument, UploadCSVDocumentImpl>();
            services.AddScoped<IUploadClientCSVDocument, UploadClientCSVDocumentImpl>();
            services.AddScoped<IClientTransaction, ClientTransactionImpl>();
            services.AddScoped<IFund, FundImpl>();
            services.AddScoped<IPortfolio, PortfolioImpl>();
            services.AddScoped<IDisclaimer, DisclaimerImpl>();
            services.AddScoped<IFactSheet, FactSheetImpl>();
            services.AddScoped<IBirthdayReports, BirthdayReportsImpl>();
            services.AddScoped<IRole, RoleImpl>();
            services.AddScoped<IFundAdministrationClient, FundAdministrationClientImpl>();
            services.AddScoped<ICurrency, CurrencyImpl>();
            services.AddScoped<IOffshore, OffshoreImpl>();
            services.AddScoped<IFundPricing, FundPricingImpl>();
            services.AddScoped<IClientStatement, ClientStatementImpl>();
            services.AddScoped<IFundBenchMark, FundBenchMarkImpl>();
            services.AddScoped<IFees, FeesImpl>();
            services.AddScoped<IReportPortfolio, ReportPortfolioImpl>();
            services.AddScoped<IMeeting, MeetingImpl>();
            services.AddScoped<IAllReports, AllReportsImpl>();
            services.AddScoped<IRunFees, RunFeesImpl>();
            services.AddScoped<IFundAdministrationDashBoard, FundAdministrationDashBoardImpl>();
            services.AddScoped<IDashBoard, DashBoardImpl>();
           
        }

    }
}
