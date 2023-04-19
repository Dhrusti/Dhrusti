using BussinesLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;
using System.Collections.Generic;

namespace Authentication_MicroService
{
	public static class ServiceExtensions
	{
		public static void DIScopes(this IServiceCollection services)
		{
			//Helpers
			services.AddScoped<CommonRepo>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//BLL
			services.AddScoped<AuthenticationBLL>();

			//Services
			services.AddScoped<IAuthentication, ImplAuthentication>();
		}
	}
}
