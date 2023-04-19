
using AutoMapper;
using BookManagement.Models;
using DTOs;

namespace BookManagement
{
    public class IAutoMapperProfile:Profile
    {
        public IAutoMapperProfile()
        {
            CreateMap<ReportDTO, ReportModel>();
            CreateMap<CategoryDTO, CategoryModel>();
            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<SubCategoryViewDTO, SubCategoryModel>();
            CreateMap<SubCategoryViewModel, SubCategoryViewDTO>();
            CreateMap<SubCategoryViewDTO, SubCategoryViewModel>();
            CreateMap<SubCategoryDTO, SubCategoryModel>();
            CreateMap<SubCategoryViewModel, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, SubCategoryViewModel>();
            CreateMap<SubCategoryModel, SubCategoryDTO>();
            CreateMap<BookDTO, BookModel>();
            CreateMap<BookModel, BookDTO>();
            CreateMap<EditBookModel, BookViewDTO>();
            CreateMap<BookViewDTO, EditBookModel>();
            CreateMap<BookViewModel, EditBookModel>();
            CreateMap<EditBookModel, BookViewModel>();
            CreateMap<BookViewModel, BookViewDTO>();
            CreateMap<BookViewDTO, BookViewModel>();
            CreateMap<BookModel, BookViewModel>();
            CreateMap<BookModel, BookViewDTO>();
            CreateMap<BookViewDTO, BookListViewModel.MainModel>();
            CreateMap<BookViewDTO, BookModel>();
            CreateMap<UserMstModel, UserMstDTO>();
            CreateMap<UserMstDTO, UserMstModel>();
            CreateMap<LoginDTO, LoginModel>();
            CreateMap<LoginModel, LoginDTO>();
            CreateMap<BookDownloadModel,BookDownloadDTO>();
            CreateMap<UserPermissionDTO, UserPermissionModel>();
            CreateMap<UserPermissionModel, UserPermissionDTO>();
            CreateMap<PermissionDTO, PermissionModel>();
            CreateMap<PermissionModel, PermissionDTO>();
            CreateMap<UserAccessPermissionModel, UserAccessPermissionDTO>();
            CreateMap<UserAccessPermissionDTO, UserAccessPermissionModel>();

            CreateMap<ForgotPasswordDTO, ForgotPasswordModel>();

            CreateMap<ForgotPasswordModel, ForgotPasswordDTO>();


            CreateMap<ChangePasswordDTO, ChangePasswordModel>();

            CreateMap<ChangePasswordModel, ChangePasswordDTO>();
        }
    }
}
