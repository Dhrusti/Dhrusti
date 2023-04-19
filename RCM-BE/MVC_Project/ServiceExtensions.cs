using BussinessLayer;
using DataLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace MVC_Project
{
	public static class ServiceExtensions
	{
		public static void DIScopes(this IServiceCollection services)
		{
			//Helpers
			services.AddScoped<CommonHelper>();
			services.AddScoped<AuthRepo>();
			services.AddScoped<CommonRepo>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//BLL
			services.AddScoped<AuthBLL>();
			services.AddScoped<AdminDashboardBLL>();

			//Services
			services.AddScoped<IAuth, AuthImpl>();
			services.AddScoped<IAdminDashboard, ImplAdminDashboard>();
		}
	}
}
