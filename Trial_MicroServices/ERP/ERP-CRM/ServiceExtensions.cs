using BussinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace ERP_CRM
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
			services.AddScoped<AuthenticationBLL>();
			services.AddScoped<DropDownBLL>();
			services.AddScoped<RequirementBLL>();

			//Services
			services.AddScoped<IUser, ImplUser>();
			services.AddScoped<IAuthentication, ImplAuthentication>();
			services.AddScoped<IDropDown, ImplDropDown>();
			services.AddScoped<IRequirement, ImplRequirement>();
		}

	}
}
