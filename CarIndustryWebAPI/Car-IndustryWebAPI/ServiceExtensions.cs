using BusinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace Car_IndustryWebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helpers
            services.AddScoped<CommonRepo>();

            //BLL
            services.AddScoped<CarBLL>();
           

            //Services
            services.AddScoped<ICar, ImplCar>();
           
        }
    }
}
