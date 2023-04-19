using BussinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI
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
            services.AddScoped<AuthBLL>();
            services.AddScoped<ClientBLL>();
            services.AddScoped<DoctorBLL>();
            services.AddScoped<PatientBLL>();
            services.AddScoped<CallTypeBLL>();
            services.AddScoped<ExtensionBLL>();
            services.AddScoped<UserBLL>();
            services.AddScoped<DurationBLL>();
            services.AddScoped<EmailBLL>();
            services.AddScoped<PatientPDFBLL>();
            services.AddScoped<NotificationBLL>();
            services.AddScoped<AdminDashboardBLL>();
            //services.AddScoped<PatientPDFBLL>();
            
            //services
            services.AddScoped<IAuth, AuthImpl>();
            services.AddScoped<IClient, ClientImpl>();
            services.AddScoped<IDoctor, DoctorImpl>();
            services.AddScoped<IPatient, PatientImpl>();
            services.AddScoped<ICallType, CallTypeImpl>();
            services.AddScoped<IExtension, ExtensionImpl>();
            services.AddScoped<IUser, UserImpl>();
            services.AddScoped<IDuration, DurationImpl>();
            services.AddScoped<IEmail, EmailImpl>();
            services.AddScoped<IPatientPDF, PatientPDFImpl>();
            services.AddScoped<INotification, NotificationImpl>();
            services.AddScoped<IAdminDashboard, AdminDashboardImpl>();
            //services.AddScoped<IPatientPDF, PatientPDFImpl>();
        }
    }
}
