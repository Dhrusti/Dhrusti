using BussinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace ERP_1.O
{
	public static class ServiceExtensions
	{
		public static void DIScopes(this IServiceCollection services)
		{
			//Helpers
			services.AddScoped<CommonRepo>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//BLL
			services.AddScoped<UserBLL>();
			

			//Services
			services.AddScoped<IUser, ImplUser>();
		
		}
	}
}
