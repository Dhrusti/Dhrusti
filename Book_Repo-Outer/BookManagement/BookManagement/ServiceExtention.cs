using BusinessLayer;
using BussinessLayer;
using ServiceLayer.IService;
using ServiceLayer.ServiceImpl;

namespace BookManagement
{
    public static class ServiceExtention
    {
        public static void RegisterDIService(this IServiceCollection objIServiceCollection)
        {
            objIServiceCollection.AddScoped<ICategory, CategoryImpl>();
            objIServiceCollection.AddScoped<CategoryBLL>();

            objIServiceCollection.AddScoped<ISubCategory, SubCategoryImpl>();
            objIServiceCollection.AddScoped<SubCategoryBLL>();

            objIServiceCollection.AddScoped<IBook, BookImpl>();
            objIServiceCollection.AddScoped<BookBLL>();

            objIServiceCollection.AddScoped<IAuthentication, AuthenticationImpl>();
            objIServiceCollection.AddScoped<AuthenticationBLL>();

            objIServiceCollection.AddScoped<IBookDownload, BookDownloadImpl>();
            objIServiceCollection.AddScoped<BookDownloadBLL>();

            objIServiceCollection.AddScoped<IAccessPermission, AccessPermissionImpl>();
            objIServiceCollection.AddScoped<AccessPermissionBLL>();

            objIServiceCollection.AddScoped<IUserProfile, UserProfileImpl>();
            objIServiceCollection.AddScoped<UserProfileBLL>();

        }
    }
}
