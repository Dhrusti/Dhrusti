using BusinessLayer;
using DTOs;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImpl
{
    public class SubCategoryImpl : ISubCategory
    {
        private readonly SubCategoryBLL _SubCategoryBLL;
        public SubCategoryImpl(SubCategoryBLL SubCategoryBLL)
        {
            this._SubCategoryBLL = SubCategoryBLL;
        }
        public ResponseDTO GetCategory()
        {
            return _SubCategoryBLL.GetCategory();
        }
        public ResponseDTO GetSubCategory()
        {
            return _SubCategoryBLL.GetSubCategory();
        }
        public ResponseDTO CreateSubCategory(SubCategoryDTO subcategory)
        {
            return _SubCategoryBLL.CreateSubCategory(subcategory);
        }
        public ResponseDTO UpdateSubCategory(int id)
        {
            return _SubCategoryBLL.UpdateSubCategory(id);
        }
        public ResponseDTO UpdateSubCategory(SubCategoryDTO subcategory)
        {
            return _SubCategoryBLL.UpdateSubCategory(subcategory);
        }
        public ResponseDTO DeleteSubCategory(int id)
        {
            return _SubCategoryBLL.DeleteSubCategory(id);
        }

    }
}
