using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface ISubCategory
    {
        public ResponseDTO GetSubCategory();
        public ResponseDTO GetCategory();
        public ResponseDTO CreateSubCategory(SubCategoryDTO subcategory);
        public ResponseDTO UpdateSubCategory(int id);
        public ResponseDTO UpdateSubCategory(SubCategoryDTO subCategory);
        public ResponseDTO DeleteSubCategory(int id);
    }
}
