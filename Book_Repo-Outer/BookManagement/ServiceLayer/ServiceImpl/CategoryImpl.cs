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
    public class CategoryImpl : ICategory
    {
        private readonly CategoryBLL _CategoryBLL;
        public CategoryImpl(CategoryBLL CategoryBLL)
        {
            this._CategoryBLL = CategoryBLL;
        }
        public ResponseDTO GetCategory()
        {
            return _CategoryBLL.GetCategory();
        }
        public ResponseDTO CreateCategory(CategoryDTO category)
        {
            return _CategoryBLL.CreateCategory(category);
        }
        public ResponseDTO UpdateCategory(int id)
        {
            return _CategoryBLL.UpdateCategory(id);
        }
        public ResponseDTO UpdateCategory(CategoryDTO category)
        {
            return _CategoryBLL.UpdateCategory(category);
        }
        public ResponseDTO DeleteCategory(int id)
        {
            return _CategoryBLL.DeleteCategory(id);
        }
    }
}
